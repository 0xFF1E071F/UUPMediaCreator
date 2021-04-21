﻿// Copyright (c) Gustave Monce and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using CommandLine;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using WindowsUpdateLib;

namespace UUPDownload
{
    internal class Program
    {
        private static void PrintLogo()
        {
            Logging.Log($"UUPDownload {Assembly.GetExecutingAssembly().GetName().Version} - Download from the Microsoft Unified Update Platform");
            Logging.Log("Copyright (c) Gustave Monce and Contributors");
            Logging.Log("https://github.com/gus33000/UUPMediaCreator");
            Logging.Log("");
            Logging.Log("This program comes with ABSOLUTELY NO WARRANTY.");
            Logging.Log("This is free software, and you are welcome to redistribute it under certain conditions.");
            Logging.Log("");
        }

        private static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            Parser.Default.ParseArguments<DownloadRequestOptions, DownloadReplayOptions, GetBuildsOptions>(args).MapResult(
              (DownloadRequestOptions opts) =>
              {
                  PrintLogo();
                  return DownloadRequest.Process.ParseOptions(opts);
              },
              (DownloadReplayOptions opts) =>
              {
                  PrintLogo();
                  return DownloadRequest.Process.ParseReplayOptions(opts);
              },
              (GetBuildsOptions opts) =>
              {
                  PrintLogo();
                  return RingCheck.ParseOptions(opts);
              },
              errs => 1);
        }
    }
}