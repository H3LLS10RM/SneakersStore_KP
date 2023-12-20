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
        public void ExportToExcel(DataGrid dataGrid)
        {
            try
            {
                var excelPackage = new ExcelPackage();
                var Worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                // Получаем коллекцию объектов UserViewModel из DataGrid
                var userList = dataGrid.Items.Cast<UserViewModel>().ToList();

                // Записываем заголовки столбцов
                for (int i = 1; i <= dataGrid.Columns.Count; i++)
                {
                    Worksheet.Cells[1, i].Value = dataGrid.Columns[i - 1].Header;
                }

                // Записываем данные
                for (int row = 0; row < dataGrid.Items.Count; row++)
                {
                    var user = userList[row]; // Получаем текущего пользователя

                    Worksheet.Cells[row + 2, 1].Value = user.id;
                    Worksheet.Cells[row + 2, 2].Value = user.name;
                    Worksheet.Cells[row + 2, 3].Value = user.surname;
                    Worksheet.Cells[row + 2, 4].Value = user.role.role_name;
                    Worksheet.Cells[row + 2, 5].Value = user.sex;
                    Worksheet.Cells[row + 2, 6].Value = user.status.status_name;
                    Worksheet.Cells[row + 2, 7].Value = user.login;
                    Worksheet.Cells[row + 2, 8].Value = user.password;
                }
                // Настройка стиля ячеек Excel.
                Worksheet.Cells.Style.Font.Name = "Times New Roman";
                Worksheet.Cells.Style.Font.Size = 12;
                Worksheet.Cells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                Worksheet.Cells.AutoFitColumns();// Автоматическая подгонка ширины колонок.
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
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка");
            }
        }

    }
}
