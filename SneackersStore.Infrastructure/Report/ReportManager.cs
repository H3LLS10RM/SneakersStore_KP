using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using SneackersStore.Infrastructure.ViewModels;

namespace SneackersStore.Infrastructure.Report
{
    public class ReportManager
    {
        static ReportManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void ExportToExcel<T>(DataGrid dataGrid, List<T> itemList) where T : class
        {
            var excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            var properties = typeof(T).GetProperties();

            // Записываем заголовки столбцов
            for (int i = 1; i <= properties.Length; i++)
            {
                worksheet.Cells[1, i].Value = properties[i - 1].Name;
            }

            // Записываем данные
            for (int row = 0; row < itemList.Count; row++)
            {
                var item = itemList[row];

                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyName = properties[i].Name;

                    // Пропускаем свойства role1Name и status1Name
                    if (propertyName == "role1Name" || propertyName == "status1Name")
                    {
                        continue;
                    }

                    var propertyValue = properties[i].GetValue(item);

                    if (propertyValue is RoleEntity roleEntity)
                    {
                        var rolePropertyName = typeof(RoleEntity).GetProperty("role_name");
                        if (rolePropertyName != null)
                        {
                            var rolePropertyValue = rolePropertyName.GetValue(roleEntity);
                            worksheet.Cells[row + 2, i + 1].Value = rolePropertyValue;
                        }
                    }
                    else if (propertyValue is StatusEntity statusEntity)
                    {
                        var statusPropertyName = typeof(StatusEntity).GetProperty("status_name");
                        if (statusPropertyName != null)
                        {
                            var statusPropertyValue = statusPropertyName.GetValue(statusEntity);
                            worksheet.Cells[row + 2, i + 1].Value = statusPropertyValue;
                        }
                    }
                    else
                    {
                        worksheet.Cells[row + 2, i + 1].Value = propertyValue;
                    }
                }
            }

            // Сохраняем файл
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = new FileInfo(saveFileDialog.FileName);
                excelPackage.SaveAs(file);
                MessageBox.Show("Данные успешно экспортированы в Excel.", "Успех");
            }
        }

    }
}