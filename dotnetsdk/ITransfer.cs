using payfurl.sdk.Models;
using System.Collections.Generic;

namespace payfurl.sdk
{
    public interface ITransfer
    {
        List<TransferData> Create(NewTransferGroup newTransfer);
        TransferList Search(TransferSearch searchData);
        TransferData Single(string transferId);
    }
}