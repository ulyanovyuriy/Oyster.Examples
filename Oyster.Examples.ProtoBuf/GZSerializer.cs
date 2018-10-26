using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Oyster.Examples.ProtoBuf
{
    public class GZSerializer
    {
        private Action<Stream, object> _write;

        private Func<Stream, object> _read;

        public GZSerializer(Action<Stream, object> Write, Func<Stream, object> Read)
        {
            _write = Write;
            _read = Read;
        }

        public void Serialize(Stream stream, object graph)
        {
            using (var gz = new GZipStream(stream, CompressionMode.Compress, true))
            {
                _write(gz, graph);
            }
        }

        public object Deserialize(Stream stream)
        {
            using (var gz = new GZipStream(stream, CompressionMode.Decompress, true))
            {
                var graph = _read(gz);
                return graph;
            }
        }
    }
}
