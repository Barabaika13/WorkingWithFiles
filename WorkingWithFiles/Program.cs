namespace WorkingWithFiles
{
    internal class Program
    {
        private const int numberOfFiles = 500;
        static void Main(string[] args)
        {
            var path1 = @"C:\Otus\TestDir1";
            DirectoryInfo dirInfo1 = new DirectoryInfo(path1);
            dirInfo1.Create();

            var path2 = @"C:\Otus\TestDir2";
            DirectoryInfo dirInfo2 = new DirectoryInfo(path2);
            dirInfo2.Create();           

            string[] fileNames = GenerateFileNames(numberOfFiles);
            CreateAndWriteFiles(path1, fileNames);
            CreateAndWriteFiles(path2, fileNames);
            ReadFiles(path1, fileNames);
            Console.WriteLine();
            ReadFiles(path2, fileNames);
        }     

        public static string[] GenerateFileNames(int numberOfFiles)
        {
            int numberOfDigits = numberOfFiles.ToString().Length;            
            string[] fileNames = new string[numberOfFiles];
            for (int i = 0; i < numberOfFiles; i++)
            {
                int value = i + 1;               
                string valueWithZeroes = value.ToString("D" + numberOfDigits.ToString());                
                fileNames[i] = $"file{valueWithZeroes}.txt";
            }
            return fileNames;
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