using ProyectoVeterinaria.Models;
//using ProyectoVeterinaria.Models.ReportViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoVeterinaria.Services
{
    public interface ICommon
    {
        string UploadedFile(IFormFile ProfilePicture);
        //Task<SMTPEmailSetting> GetSMTPEmailSetting();
        // Task<SendGridSetting> GetSendGridEmailSetting();
        FacturacionViewModel GetTranSummaryAndDetails(int TranId);   // InvoiceReporViewModel GetInvoiceReportData(string _TranId);
       // InvoiceSummary GetByTranSummary(string _TranId);
       // string GenItemCode(string CompanyName);
       void CurrentItemsUpdate(Int64 Id, int SelectQuantity, bool IsAddition, string ActionMessage, string AppUser);
       // IQueryable<ItemDropdownListViewModel> LoadddlInventoryItem();
        IQueryable<FacturacionViewModel> GetInvoiceList();
        void CurrentItemsUpdate(Producto producto, int cantidad, bool v1, string v2, string? name);
    }
}
