// Map.Downloader
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class Downloader
{
    public int BytesLoaded;

    public int BytesTotal;

    public byte[] Data;

    public bool Finished;

    public string Error = "";

    public float Progress;

    protected string url;

    public bool Loading;

    public Downloader(string url, bool startInstant = false)
    {
        this.url = url;
        if (startInstant)
        {
            StartDownload();
        }
    }

    public void StartDownload()
    {
        Loading = true;
        new Thread(Download).Start();
    }

    private void Download()
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, (RemoteCertificateValidationCallback)((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true));
                webClient.DownloadProgressChanged += DownloadProgressChanged;
                webClient.DownloadDataCompleted += DownloadDataCompleted;
                webClient.DownloadDataAsync(new Uri(url));
            }
        }
        catch (Exception ex)
        {
            Error = ex.ToString();
        }
    }

    private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
    {
        Data = e.Result;
        Finished = true;
    }

    private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        BytesLoaded += Convert.ToInt32(e.BytesReceived);
        BytesTotal = Convert.ToInt32(e.TotalBytesToReceive);
    }
}
