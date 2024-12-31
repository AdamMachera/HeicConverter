using ImageMagick;

internal class Program
{
    private static void Main(string[] args)
    {
        string sourcePath = args[0];
        string destPath = args[1];

        if (!Directory.Exists(sourcePath))
        {
            throw new InvalidOperationException($"Directory {sourcePath} doesn't exist");
        }

        if (!Directory.Exists(destPath))
        {
            Directory.CreateDirectory(destPath);
        }

        Console.WriteLine($"Converting files from {sourcePath} to {destPath}");

        string[] allfiles = Directory.GetFiles(sourcePath, "*.heic", SearchOption.TopDirectoryOnly);

        foreach (var file in allfiles)
        {
            FileInfo info = new FileInfo(file);
            using (MagickImage image = new MagickImage(info.FullName))
            {
                // Save frame as jpg
                image.Write($"{destPath}{System.IO.Path.PathSeparator}{Path.GetFileNameWithoutExtension(info.Name)}.jpg");
                Console.WriteLine($"Converted {info.Name}");
            }
        }
    }
}