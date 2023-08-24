namespace WorkingWithFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path1 = @"C:\Otus\TestDir1";
            DirectoryInfo dirInfo1 = new DirectoryInfo(path1);
            dirInfo1.Create();

            var path2 = @"C:\Otus\TestDir2";
            DirectoryInfo dirInfo2 = new DirectoryInfo(path2);
            dirInfo2.Create();

            string[] fileNames = new string[]
            {
                "file1.txt",
                "file2.txt",
                "file3.txt",
                "file4.txt",
                "file5.txt",
                "file6.txt",
                "file7.txt",
                "file8.txt",
                "file9.txt",
                "file10.txt",
            };

            CreateAndWriteFiles(path1, fileNames);
            CreateAndWriteFiles(path2, fileNames);
            ReadFiles(path1, fileNames);
            Console.WriteLine();
            ReadFiles(path2, fileNames);
        }

        public static void CreateAndWriteFiles(string path, string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (!File.Exists(Path.Combine(path, fileNames[i])))
                {
                    try
                    {
                        using (var stream = File.CreateText(Path.Combine(path, fileNames[i])))
                        {
                            stream.WriteLine(fileNames[i]);
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                using (var stream = File.AppendText(Path.Combine(path, fileNames[i])))
                {
                    stream.WriteLine(DateTime.Now.ToString());
                }
            }
        }

        public static void ReadFiles(string path, string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                using (var stream = File.OpenText(Path.Combine(path, fileNames[i])))
                {
                    Console.WriteLine($"{fileNames[i]}: ");
                    while (!stream.EndOfStream)
                    {
                        var s = stream.ReadLine();
                        Console.WriteLine(s);

                    }
                }
            }
        }
    }
}