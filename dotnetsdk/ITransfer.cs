using payfurl.sdk.Models;

namespace payfurl.sdk
{
    public interface ITransfer
    {
        TransferData Create(NewTransfer newTransfer);
        TransferList Search(TransferSearch searchData);
        TransferData Single(string transferId);
    }
}