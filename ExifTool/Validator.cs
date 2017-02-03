﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExifTool
{
    public class Validator
    {
        private List<String> consoleArguments;

        public Validator(String[] args)
        {
            consoleArguments = args.ToList<String>();
        }

        public Validator(string path)
        {
            consoleArguments = new List<string>();
            consoleArguments.Add(path);
        }

        public List<String> GetAllValidPaths()
        {
            List<String> imgPathList = new List<string>();

            var argumentImgPaths = consoleArguments.FindAll(path => IsImage(path));
            var argumentDirPaths = consoleArguments.FindAll(path => IsDirectory(path));
            var imgPathsFromDir = GetAllImagesPaths(argumentDirPaths);

            imgPathList.AddRange(argumentImgPaths);
            imgPathList.AddRange(imgPathsFromDir);
            return imgPathList;
        }

        private List<String> GetImagesFromDirectory(String path)
        {
            List<String> results = new List<string>();
            var thisdir = Directory.GetFiles(path,"*.jpg", SearchOption.TopDirectoryOnly);
            results.AddRange(thisdir);

            return results;
        }

        private List<String> GetAllImagesPaths(List<String> dirPaths)
        {
            List<String> results = new List<String>();

            foreach (String directory in dirPaths)
            {
                var imgsInDir = GetImagesFromDirectory(directory);
                results.AddRange(imgsInDir);
            }
            return results;
        }

        private bool IsImage(String path)
        {
            bool fileExists = File.Exists(path);
            bool isJPG = path.ToUpperInvariant().EndsWith(".JPG");
            return fileExists && isJPG;
        }

        private bool IsDirectory(String path)
        {
            return Directory.Exists(path);
        }
    }
}
