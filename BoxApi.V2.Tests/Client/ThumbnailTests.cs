using System;
using System.Drawing;
using System.IO;
using System.Threading;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Tests.Harness;
using NUnit.Framework;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2.Tests.Client
{
    [TestFixture]
    public class ThumbnailTests : BoxApiTestHarness
    {
        [Test]
        public void GetThumbnail()
        {
            File file = null;
            try
            {
                file = PostImageFile("testimage.jpg");
                Image thumbnail = GetThumbnail(file);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Width, Is.GreaterThanOrEqualTo(32));
                Assert.That(thumbnail.Height, Is.GreaterThanOrEqualTo(32));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [TestCase(ThumbnailSize.Small)]
        [TestCase(ThumbnailSize.Medium)]
        [TestCase(ThumbnailSize.Large)]
        [TestCase(ThumbnailSize.Jumbo)]
        public void ThumbnailMinHeight(ThumbnailSize size)
        {
            File file = null;
            try
            {
                file = PostImageFile("testimage.jpg");
                Image thumbnail = GetThumbnail(file, minHeight: size);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Height, Is.GreaterThanOrEqualTo(int.Parse(size.Description())));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [TestCase(ThumbnailSize.Small)]
        [TestCase(ThumbnailSize.Medium)]
        [TestCase(ThumbnailSize.Large)]
        [TestCase(ThumbnailSize.Jumbo)]
        public void ThumbnailMaxHeight(ThumbnailSize size)
        {
            File file = null;
            try
            {
                file = PostImageFile("testimage.jpg");
                Image thumbnail = GetThumbnail(file, maxHeight: size);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Height, Is.LessThanOrEqualTo(int.Parse(size.Description())));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [TestCase(ThumbnailSize.Small, ThumbnailSize.Medium)]
        [TestCase(ThumbnailSize.Small, ThumbnailSize.Jumbo)]
        [TestCase(ThumbnailSize.Large, ThumbnailSize.Jumbo)]
        public void ThumbnailHeightBounded(ThumbnailSize minHeight, ThumbnailSize maxHeight)
        {
            File file = null;
            try
            {
                file = PostImageFile("testimage.jpg");
                Image thumbnail = GetThumbnail(file, minHeight: minHeight, maxHeight: maxHeight);
                Assert.That(thumbnail, Is.Not.Null);
                Assert.That(thumbnail.Height, Is.GreaterThanOrEqualTo(int.Parse(minHeight.Description())));
                Assert.That(thumbnail.Height, Is.LessThanOrEqualTo(int.Parse(maxHeight.Description())));
            }
            finally
            {
                Client.Delete(file);
            }
        }


        private Image GetThumbnail(File file, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null)
        {
            while (true)
            {
                try
                {
                    byte[] thumbnail = Client.GetThumbnail(file, minHeight, minWidth, maxHeight, maxWidth);
                    using (var stream = new MemoryStream(thumbnail))
                    {
                        return Image.FromStream(stream);
                    }
                }
                catch (BoxDownloadNotReadyException e)
                {
                    Console.Out.WriteLine("Waiting {0} seconds before retrying...", e.RetryAfter);
                    Thread.Sleep(e.RetryAfter*1000);
                }
            }
        }

        [Test, ExpectedException(typeof (BoxDownloadNotReadyException))]
        public void ThumbnailNotReady()
        {
            File file = null;
            try
            {
                file = PostImageFile("testimage.jpg");
                byte[] thumbnail = Client.GetThumbnail(file);
                Assert.That(thumbnail.Length, Is.Not.EqualTo(0));
            }
            finally
            {
                Client.Delete(file);
            }
        }

        [Test]
        public void GenericThumbnail()
        {
            File file = null;
            try
            {
                file = PostImageFile(TestItemName());
                byte[] thumbnail = Client.GetThumbnail(file);
                Assert.That(thumbnail.Length, Is.Not.EqualTo(0));
            }
            finally
            {
                Client.Delete(file);
            }
        }
    }
}