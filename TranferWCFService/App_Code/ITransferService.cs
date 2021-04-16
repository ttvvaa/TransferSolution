using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LawWebApplication
{
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        DownloadStorageStream DownloadDocumentStorage(DownloadStorageRequest request);

        [OperationContract]
        void UploadDocumentStorage(RemoteFileInfo request);
    }
    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string ExportFileId;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class DownloadStorageRequest
    {
        [MessageBodyMember]
        public string Customs;

        [MessageBodyMember]
        public string FileGuid;
    }
    [MessageContract]
    public class DownloadStorageStream : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public Stream Data;

        public void Dispose()
        {
            if (Data != null)
            {
                Data.Close();
                Data = null;
            }
        }
    }

    [MessageContract]
    public class UploadStorageStream
    {
        [MessageHeader(MustUnderstand = true)]
        public string Customs;

        [MessageHeader(MustUnderstand = true)]
        public string FileGuid;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public Stream Data;
    }
}
