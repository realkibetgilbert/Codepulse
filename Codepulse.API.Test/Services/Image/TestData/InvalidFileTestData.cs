using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codepulse.API.Test.Services.Image.TestData
{
    public class InvalidFileTestData
    {
        public static IEnumerable<object[]> InvalidFiles =>
        new List<object[]>
        {
            new object[] {
                new FormFile(new MemoryStream(new byte[0]), 0, 0, "Data", "badfile.txt")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/plain"
                }
            },
            new object[] {
                new FormFile(new MemoryStream(new byte[0]), 0, 0, "Data", "file.exe")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                }
            },
            new object[] {
                new FormFile(new MemoryStream(new byte[0]), 0, 0, "Data", "file.json")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/json"
                }
            }
        };
    }
}
