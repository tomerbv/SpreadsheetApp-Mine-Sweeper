using System;
using System.IO;
using System.Threading;

class SharableSpreadSheet
{
    String[,] table;
    int nRows, nCols;
    Mutex StructureMut;
    Mutex ReaderMut;
    Mutex[] RowMut;
    int[] RowReadCounter;
    Mutex[] ColMut;
    int[] ColReadCounter;
    int ReadingThreads;
    SemaphoreSlim ReadLimit;
    bool LimitSet;

    public SharableSpreadSheet(int nRows, int nCols)
    {
        this.nRows = nRows;
        this.nCols = nCols;
        this.table = new string[nRows,nCols];
        RowMut = new Mutex[nRows];
        RowReadCounter = new int[nRows];
        ColMut = new Mutex[nCols];
        ColReadCounter = new int[nCols];
        
        for (int i = 0; i < nRows; i++)
        {
            RowMut[i] = new Mutex();
            RowReadCounter[i] = 0;
        }
        for (int i = 0; i < nCols; i++)
        {
            ColMut[i] = new Mutex();
            ColReadCounter[i] = 0;
        }
        this.StructureMut = new Mutex();
        this.ReaderMut = new Mutex();
        this.ReadingThreads = 0;
        LimitSet = false;
    }

    private void RangeReadPermission(int row1, int row2, int col1, int col2)
    {
        ReaderMut.WaitOne();
        if (LimitSet)
            ReadLimit.Wait();
        if((row2 - row1) < (col2 - col1))
        {
            for (int i = row1; i <= row2; i++)
            {
                RowMut[i].WaitOne();
                Interlocked.Increment(ref RowReadCounter[i]);
                RowMut[i].ReleaseMutex();
            }
        }
        else
        {
            for (int j = col1; j <= col2; j++)
            {
                ColMut[j].WaitOne();
                Interlocked.Increment(ref ColReadCounter[j]);
                ColMut[j].ReleaseMutex();
            }
        }
        Interlocked.Increment(ref ReadingThreads);
        ReaderMut.ReleaseMutex();
    }
    private void RangeReadRelease(int row1, int row2, int col1, int col2)
    {
        if ((row2 - row1) < (col2 - col1))
        {
            for (int i = row1; i <= row2; i++)
                Interlocked.Decrement(ref RowReadCounter[i]);
        }
        else
        {
            for (int j = col1; j <= col2; j++)
                Interlocked.Decrement(ref ColReadCounter[j]);
        }
        if (LimitSet)
            ReadLimit.Release();
        Interlocked.Decrement(ref ReadingThreads);


    }
    public String getCell(int row, int col)
    {
        // return the string at [row,col]
        if (row > nRows || row < 1 || col > nCols || col < 1)
            return null;
        RangeReadPermission(row - 1, row - 1, col - 1, col - 1);
        String res = table[row - 1, col - 1];
        RangeReadRelease(row - 1, row - 1, col - 1, col - 1);
        if (res == "")
            return null;
        return res;
    }

    private void CellWritePermission(int row, int col)
    {
        ReaderMut.WaitOne();
        StructureMut.WaitOne();
        RowMut[row].WaitOne();
        ColMut[col].WaitOne();
        while (RowReadCounter[row] > 0 || ColReadCounter[col] > 0)
        {
            Thread.Sleep(ReadingThreads);
        }
        ReaderMut.ReleaseMutex();
    }
    private void CellWriteRelease(int row, int col)
    {
        RowMut[row].ReleaseMutex();
        ColMut[col].ReleaseMutex();
        StructureMut.ReleaseMutex();
    }
    public bool setCell(int row, int col, String str)
    {
        if (row > nRows || row < 1 || col > nCols || col < 1)
            return false;
        CellWritePermission(row - 1, col - 1);
        table[row - 1, col - 1] = str;
        CellWriteRelease(row - 1, col - 1);
        return true;
    }


    public bool searchString(String str, ref int row, ref int col)
    {
        // search the cell with string str, and return true/false accordingly.
        // stores the location in row,col.
        // return the first cell that contains the string (search from first row to the last row)
        return searchInRange(1, nCols, 1, nRows,  str, ref row, ref col);
    }

    private void StructurePermission()
    {
        ReaderMut.WaitOne();
        StructureMut.WaitOne();
        while (this.ReadingThreads > 0)
        {
            Thread.Sleep(ReadingThreads);
        }
    }
    private void StructureRelease()
    {
        ReaderMut.ReleaseMutex();
        StructureMut.ReleaseMutex();
    }
    public bool exchangeRows(int row1, int row2)
    {
        // exchange the content of row1 and row2
        if (row1 > nRows || row1 < 1 || row2 > nRows || row2 < 1)
            return false;
        ReaderMut.WaitOne();
        StructureMut.WaitOne();
        RowMut[row1 - 1].WaitOne();
        RowMut[row2 - 1].WaitOne();
        ReaderMut.ReleaseMutex();
        while (RowReadCounter[row1 - 1] > 0 || RowReadCounter[row2 - 1] > 0)
        {
            Thread.Sleep(1);
        }
        String temp;
        for (int i = 0; i < nCols; i++)
        {
            temp = this.table[row1 - 1, i];
            this.table[row1 - 1, i] = this.table[row2 - 1, i];
            this.table[row2 - 1, i] = temp;
        }
        StructureMut.ReleaseMutex();
        RowMut[row1 - 1].ReleaseMutex();
        RowMut[row2 - 1].ReleaseMutex();
        return true;
    }
    public bool exchangeCols(int col1, int col2)
    {
        // exchange the content of col1 and col2
        if (col1 > nCols || col1 < 1 || col2 > nCols || col2 < 1)
            return false;
        ReaderMut.WaitOne();
        StructureMut.WaitOne();
        ColMut[col1 - 1].WaitOne();
        ColMut[col2 - 1].WaitOne();
        ReaderMut.ReleaseMutex();
        while (ColReadCounter[col1 - 1] > 0 || ColReadCounter[col2 - 1] > 0)
        {
            Thread.Sleep(1);
        }
        String temp;
        for (int i = 0; i < nRows; i++)
        {
            temp = this.table[i, col1 - 1];
            this.table[i, col1 - 1] = this.table[i, col2 - 1];
            this.table[i, col2 - 1] = temp;
        }
        StructureMut.ReleaseMutex();
        ColMut[col1 - 1].ReleaseMutex();
        ColMut[col2 - 1].ReleaseMutex();
        return true;
    }
    public bool searchInRow(int row, String str, ref int col)
    {
        // perform search in specific row
        if (row > nRows || row < 1 )
            return false;
        return searchInRange(1, nCols, row, row + 1, str, ref row, ref col);
    }
    public bool searchInCol(int col, String str, ref int row)
    {
        // perform search in specific col
        if (col > nCols || col < 1)
            return false;
        return searchInRange(col, col + 1, 1, nRows, str, ref row, ref col);
    }
    public bool searchInRange(int col1, int col2, int row1, int row2, String str, ref int row, ref int col)
    {
        // perform search within spesific range: [row1:row2,col1:col2] 
        //includes col1,col2,row1,row2
        if ( row1 > nRows || row1 < 1 || row2 > nRows || row2 < 1 || col1 > nCols || col1 < 1 || col2 > nCols || col2 < 1)
            return false;
        RangeReadPermission(row1 - 1, row2 - 1, col1 - 1, col2 - 1);
        for (int i = row1; i <= row2; i++)
        {
            for(int j = col1; j <= col2; j++)
            {
                if (str == table[i - 1, j - 1])
                {
                    row = i;
                    col = j;
                    RangeReadRelease(row1 - 1, row2 - 1, col1 - 1, col2 - 1);
                    return true;
                }
            }
        }
        RangeReadRelease(row1 - 1, row2 - 1, col1 - 1, col2 - 1);
        return false;
    }
    public bool addRow(int row1)
    {
        //add a row after row1
        if (row1 > nRows || row1 < 0)
            return false;
        StructurePermission();
        int spacemaker = 0;
        String[,] new_table = new string[this.nRows + 1, this.nCols];
        for (int i = 0; i < this.nRows; i++)
        {
            if (i == row1)
                spacemaker++;
            for (int j = 0; j < this.nCols; j++)
            {
                new_table[i + spacemaker, j] = this.table[i, j];
            }
        }
        this.table = new_table;
        Interlocked.Increment(ref this.nRows);
        RowMut = new Mutex[nRows];
        RowReadCounter = new int[nRows];
        for (int i = 0; i < nRows; i++)
        {
            RowMut[i] = new Mutex();
            RowReadCounter[i] = 0;
        }
        StructureRelease();
        return true;
    }
    public bool addCol(int col1)
    {
        //add a column after col1
        if (col1 > nCols || col1 < 0)
            return false;
        StructurePermission();
        int spacemaker = 0;
        String[,] new_table = new string[this.nRows, this.nCols + 1];
        for (int j = 0; j < this.nCols; j++)
        {
            if (j == col1)
                spacemaker++;
            for (int i = 0; i < this.nRows; i++)
            {
                new_table[i, j + spacemaker] = this.table[i, j];
            }
        }
        this.table = new_table;
        Interlocked.Increment(ref this.nCols);
        ColMut = new Mutex[nCols];
        ColReadCounter = new int[nCols];
        for (int i = 0; i < nCols; i++)
        {
            ColMut[i] = new Mutex();
            ColReadCounter[i] = 0;
        }
        StructureRelease();
        return true;
    }
    public void getSize(ref int nRows, ref int nCols)
    {
        StructureMut.WaitOne();
        nRows = this.nRows;
        nCols = this.nCols;
        StructureMut.ReleaseMutex();
    }
    public bool setConcurrentSearchLimit(int nUsers)
    {
        // this function aims to limit the number of users that can perform the search operations concurrently.
        // The default is no limit. When the function is called, the max number of concurrent search operations is set to nUsers. 
        // In this case additional search operations will wait for existing search to finish.

        if (nUsers < 1)
            return false;
        StructurePermission();
        while (this.ReadingThreads > nUsers)
        {
            Thread.Sleep(ReadingThreads);
        }
        this.ReadLimit = new SemaphoreSlim(nUsers - ReadingThreads, nUsers);
        this.LimitSet = true;
        StructureRelease();
        return true;
    }

    public bool save(String fileName)
    {
        // save the spreadsheet to a file fileName.
        // you can decide the format you save the data. There are several options.
        StructurePermission();
        if (!fileName.Contains(".dat"))
            fileName = fileName + ".dat";
        if (File.Exists(fileName))
            File.Delete(fileName);
        using(StreamWriter Writer = File.AppendText(fileName))
        {
            Writer.WriteLine(this.nRows);
            Writer.WriteLine(this.nCols);
            for (int i = 0; i < this.nRows; i++)
            {
                for (int j = 0; j < this.nCols; j++)
                {
                    Writer.WriteLine(this.table[i, j]);
                }
            }
        }
        StructureRelease();
        return true;
    }
    public bool load(String fileName)
    {
        // load the spreadsheet from fileName
        // replace the data and size of the current spreadsheet with the loaded data
        StructurePermission();
        if (!File.Exists(fileName))
            return false;
        using (StreamReader Reader = new StreamReader(fileName))
        {
            this.nRows = Int32.Parse(Reader.ReadLine());
            this.nCols = Int32.Parse(Reader.ReadLine());
            this.table = new string[nRows, nCols];
            RowMut = new Mutex[nRows];
            RowReadCounter = new int[nRows];
            ColMut = new Mutex[nCols];
            ColReadCounter = new int[nCols];
            for (int i = 0; i < nRows; i++)
            {
                RowMut[i] = new Mutex();
                RowReadCounter[i] = 0;
            }
            for (int i = 0; i < nCols; i++)
            {
                ColMut[i] = new Mutex();
                ColReadCounter[i] = 0;
            }
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    this.table[i, j] = Reader.ReadLine();
                }
            }
        }
        StructureRelease();
        return true;
    }

    public void FillWithGarbage()
    {
        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < nCols; j++)
            {
                table[i, j] = "testcell" + i.ToString() + j.ToString();
            }
        }
    }
}



