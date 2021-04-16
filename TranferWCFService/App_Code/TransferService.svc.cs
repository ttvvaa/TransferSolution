//using Law.Data;
//using Law.Interact.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace LawWebApplication
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TransferService : ITransferService
    {
        //private static readonly InteractWebProperties webProps = new InteractWebProperties();
        //      private static readonly LawSessionManager sessions = new LawSessionManager(webProps.DbmsType, webProps.ProviderType, webProps.InteractSource);
        //private static readonly InteractServer server = new InteractServer(sessions, webProps.CardsSource, webProps.RifSource, Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data"));

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            //server.GetInteractManager().GetFileBodyBlob(request.ExportFileId, out Stream stream, out string fileName, out int length);
            Stream stream = new MemoryStream();
            string fileName = "file name";
            int length = 0;
            return new RemoteFileInfo()
            {
                FileByteStream = stream,
                FileName = fileName,
                Length = length
            };
        }

        public DownloadStorageStream DownloadDocumentStorage(DownloadStorageRequest req)
        {
            FileInfo info = new FileInfo(Path.Combine(//webProps.StorageDirPath,
                req.Customs, req.FileGuid + ".dat"));
            return new DownloadStorageStream()
            {
                Length = info.Length,
                Data = new FileStream(info.FullName, FileMode.Open, FileAccess.Read)
            };
        }

        public void UploadDocumentStorage(RemoteFileInfo request)
        {
            FileInfo info = new FileInfo(Path.Combine(//webProps.StorageDirPath,
                                                      @"c:\temp", "10000000", request.FileName));
            if (info.Exists) info.Delete();
            if (!info.Directory.Exists) info.Directory.Create();
            var readStream = request.FileByteStream;
            using (var output = new FileStream(info.FullName, FileMode.CreateNew, FileAccess.Write))
            {
                readStream.CopyTo(output);
                output.Close();
                readStream.Close();
            }
        }

        /*
        public void UploadFile(RemoteFileInfo request)
        {
            FileStream targetStream = null;
            Stream sourceStream = request.FileByteStream;

            string uploadFolder = @"D:\temp\upload\";

            string filePath = Path.Combine(uploadFolder, request.FileName);

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int bufferLen = 65000;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
        }
        */
    }
}
