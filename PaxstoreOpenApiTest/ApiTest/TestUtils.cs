﻿using log4net;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Help;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestUtils
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestUtils));

        const string base64Content = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/4QM+RXhpZgAASUkqAKwCAABgAAAAAQAAAGAAAAABAAAAQwBoAHUAYwBrACAAQQBuAGQAZQByAHMAbwBuACAAJgAgAEUAcgBpAGsAIABBAHQAdABrAGkAcwBzAG8AbgAgAG8AZgAgAE4AbwBQAGEAdAB0AGUAcgBuAC4AYwBvAG0AAAAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAAAAAAQEIPAAAAAABAQg8AAAAAAEBCDwAaAJqCBQABAAAAAgEAAJ2CBQABAAAACgEAACKIAwABAAAAAAAAACeIAwACAAAAAAAAAACQBwAEAAAAMDIxMAKRBQABAAAAEgEAAAGSBQABAAAAGgEAAAKSBQABAAAAIgEAAAOSBQABAAAAKgEAAASSBQABAAAAMgEAAAWSBQABAAAAOgEAAAaSBQABAAAAQgEAAAeSAwABAAAAAAAAAAiSAwABAAAAAAAAAAmSAwABAAAAAAAAAAqSBQABAAAASgEAAAGgAwABAAAAAAAAAAKgAwABAAAAgAAAAAOgAwABAAAAdQAAAA6iBQABAAAAUgEAAA+iBQABAAAAWgEAABCiAwABAAAAAAAAABWiBQABAAAAYgEAABeiAwABAAAAAAAAAACjAwABAAAAAAAAAAGjAwABAAAAAAAAAAAAAABqAQAACwASAQMAAQAAAAEAAAAaAQUAAQAAAAgAAAAbAQUAAQAAABAAAAAoAQMAAQAAAAIAAAA+AQUAAgAAAHoAAAA/AQUABgAAAIoAAAARAgUAAwAAALoAAAATAgMAAQAAAAAAAAAUAgUABgAAANIAAABphwQAAQAAAGoBAACdnAEAYgAAABgAAAAAAAAA/+ELrGh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8APD94cGFja2V0IGJlZ2luPSfvu78nIGlkPSdXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQnPz4NCjx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iPjxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT5DaHVjayBBbmRlcnNvbiAmYW1wOyBFcmlrIEF0dGtpc3NvbiBvZiBOb1BhdHRlcm4uY29tPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjxkYzpyaWdodHM+PHJkZjpBbHQgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj48cmRmOmxpIHhtbDpsYW5nPSJ4LWRlZmF1bHQiPsKpIDIwMDkgTWljcm9zb2Z0IENvcnBvcmF0aW9uLiAgQWxsIHJpZ2h0cyByZXNlcnZlZDwvcmRmOmxpPjwvcmRmOkFsdD4NCgkJCTwvZGM6cmlnaHRzPjwvcmRmOkRlc2NyaXB0aW9uPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iPjx0aWZmOmFydGlzdD5DaHVjayBBbmRlcnNvbiAmYW1wOyBFcmlrIEF0dGtpc3NvbiBvZiBOb1BhdHRlcm4uY29tPC90aWZmOmFydGlzdD48L3JkZjpEZXNjcmlwdGlvbj48L3JkZjpSREY+PC94OnhtcG1ldGE+DQo8P3hwYWNrZXQgZW5kPSd3Jz8+ICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAg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";

        [Test]
        public void TestBase64Utils() {
            int size = Base64FileUtil.GetBase64FileSize(base64Content);
            _logger.DebugFormat("FileSize=\n{0}", size);
            _logger.DebugFormat("FileSize=\n{0} KB", size/1024);
        }

        [Test]
        public void TestDateTime(){
            ReadOnlyCollection<TimeZoneInfo> zones = TimeZoneInfo.GetSystemTimeZones();
            for (int i = 0; i < zones.Count; i++) {
                Console.WriteLine(TimeZoneHelper.GetTimeZoneId(zones[i]));
                Assert.NotNull(TimeZoneHelper.GetTimeZoneId(zones[i]));
            }
        }
    }
}
