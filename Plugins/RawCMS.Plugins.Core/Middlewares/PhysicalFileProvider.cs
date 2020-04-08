using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace RawCMS.Plugins.Core.Middlewares
{
    public class RawCmsPhysicalFileProvider : IFileProvider, IDisposable
    {
        private Dictionary<string, PhysicalFileProvider> providerMap = new Dictionary<string, PhysicalFileProvider>();

        public RawCmsPhysicalFileProvider()
        {
            var appRoot = Path.Combine(Directory.GetCurrentDirectory(), "Plugins/RawCMS.Plugins.Core/Assets");

            //providerMap.Add("/", new PhysicalFileProvider());
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return null;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IChangeToken Watch(string filter)
        {
            return null;
        }

        public void Dispose()
        {
        }
    }
}