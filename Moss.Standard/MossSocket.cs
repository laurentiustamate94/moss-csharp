using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Moss.Standard.Models;
using Moss.Standard.Resources;

namespace Moss.Standard
{
    public class MossSocket
    {
        private Socket _socket = null;
        private readonly string _userId = null;

        public MossSocket(string userId)
        {
            this._userId = userId;
        }

        public MossResponse SendRequest(MossRequest request)
        {
            var endpoint = GetEndPoint();

            using (this._socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    this._socket.Connect(endpoint);

                    this.SendBaseOptions(request);
                    this.SendFiles(request);

                    var resultUrl = this.SendFinalOptions(request);

                    if (Uri.TryCreate(resultUrl, UriKind.Absolute, out Uri url))
                    {
                        return new MossResponse
                        {
                            ResultUrl = url.ToString(),
                            IsValid = true
                        };
                    }

                    return new MossResponse
                    {
                        IsValid = false
                    };
                }
                catch (Exception ex)
                {
                    //TODO Log this
                }
            }

            return new MossResponse
            {
                IsValid = false
            };
        }

        private IPEndPoint GetEndPoint()
        {
            var host = Dns.GetHostEntry(AppSettings.MossServer);

            return new IPEndPoint(host.AddressList.First(), int.Parse(AppSettings.MossPort));
        }

        private void SendBaseOptions(MossRequest request)
        {
            this.SendOption(AppSettings.UserIdOption, this._userId);
            this.SendOption(AppSettings.DirectoryModeOption, request.IsDirectoryMode);
            this.SendOption(AppSettings.ExperimentalOption, request.IsExperimental);
            this.SendOption(AppSettings.MaximumMatchesOption, request.MaximumMatches);
            this.SendOption(AppSettings.MaximumFilesToShowOption, request.MaximumFilesToShow);
        }

        private void SendFiles(MossRequest request)
        {
            foreach (var file in request.BaseFileNames)
            {
                this.SendFile(request.Language, file, 0);
            }

            var submittedFilesCount = request.FileNames.Count();

            for (int i = 0; i < submittedFilesCount; i++)
            {
                this.SendFile(request.Language, request.FileNames.ElementAt(i), i + 1);
            }
        }

        private string SendFinalOptions(MossRequest request)
        {
            var result = new byte[512];

            this.SendOption(AppSettings.QueryOption, request.Comments);
            this._socket.Receive(result);
            this.SendOption(AppSettings.EndOption, string.Empty);

            return Encoding.UTF8.GetString(result);
        }

        private void SendOption(string option, bool value)
        {
            this.SendOption(option, value ? "1" : "0");
        }

        private void SendOption(string option, int value)
        {
            this.SendOption(option, value.ToString(CultureInfo.InvariantCulture));
        }

        private void SendOption(string option, string value)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes($"{option} {value}\n");

                this._socket.Send(data);
            }
            catch (Exception ex)
            {
                //TODO Log this
                throw;
            }
        }

        private void SendFile(string language, string filename, int uniqueId)
        {
            try
            {
                var file = new FileInfo(filename);
                var data = Encoding.UTF8.GetBytes($"{uniqueId} {language} {file.Length} {file.FullName.Replace("\\", "/").Replace(" ", string.Empty)}\n");

                this._socket.Send(data);
            }
            catch (Exception ex)
            {
                //TODO Log this
                throw;
            }
        }
    }
}
