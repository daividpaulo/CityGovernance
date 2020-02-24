using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CityGovernance.Services.Services
{
	public class ReportsCityService : IReportsCityService
	{
		IWorkbook workbook;
		ICityRepository _cityRepository;

		public ReportsCityService(ICityRepository cityRepository)
		{
			workbook = new XSSFWorkbook();
			_cityRepository = cityRepository;
		}

		public MemoryStream CountsCitysForRegion()
		{
			ISheet sheet = workbook.CreateSheet("Qtd Cidades por Região");

			List<City> cities = _cityRepository.GetAll().ToList();

			var groupBy = cities
							  .GroupBy(x => x.Region.Name)
							  .Select(rel => new
							  {
								  Region = rel.Key,
								  QtdCidades = rel.Count()
							  }).ToList();


			int rowNumer = 0;

			//---- HEADER

			IRow row = sheet.CreateRow(rowNumer);
			ICell cell;

			ICellStyle style = workbook.CreateCellStyle();
			style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
			style.FillPattern = FillPattern.SolidForeground;

			cell = row.CreateCell(0);
			cell.SetCellValue("Região");
			cell.CellStyle = style;

			cell = row.CreateCell(1);
			cell.SetCellValue("Qtd Cidades");
			cell.CellStyle = style;

			groupBy.OrderByDescending(x=>x.QtdCidades).ToList().ForEach(item =>
			{
				rowNumer++;
				row = sheet.CreateRow(rowNumer);
				row.CreateCell(0).SetCellValue(item.Region);
				row.CreateCell(1).SetCellValue(item.QtdCidades);
			});

			rowNumer++;
			row = sheet.CreateRow(rowNumer);
			row.CreateCell(0).SetCellValue(" ");
			row.CreateCell(1).SetCellValue(" ");



			sheet.SetColumnWidth(0, 40 * 256);
			sheet.SetColumnWidth(1, 20 * 256);

			MemoryStream stream = new MemoryStream();
			workbook.Write(stream);

			return stream;
		}

		public MemoryStream CountsCitysForUF()
		{

			ISheet sheet = workbook.CreateSheet("Qtd Cidades por UF");

			List<City> cities = _cityRepository.GetAll().ToList();

			var groupByUf = cities
							  .GroupBy(x => x.Uf)
							  .Select(rel => new
							  {
								  Uf = rel.Key,
								  QtdCidades = rel.Count()
							  }).ToList();


			int rowNumer = 0;

			//---- HEADER

			IRow row = sheet.CreateRow(rowNumer);
			ICell cell;

			ICellStyle style = workbook.CreateCellStyle();
			style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
			style.FillPattern = FillPattern.SolidForeground;

			cell = row.CreateCell(0);
			cell.SetCellValue("UF");
			cell.CellStyle = style;

			cell = row.CreateCell(1);
			cell.SetCellValue("Qtd Cidades");
			cell.CellStyle = style;

			groupByUf.OrderByDescending(x => x.QtdCidades).ToList().ForEach(item =>
			{
				rowNumer++;
				row = sheet.CreateRow(rowNumer);
				row.CreateCell(0).SetCellValue(item.Uf);
				row.CreateCell(1).SetCellValue(item.QtdCidades);
			});

			rowNumer++;
			row = sheet.CreateRow(rowNumer);
			row.CreateCell(0).SetCellValue(" ");
			row.CreateCell(1).SetCellValue(" ");



			sheet.SetColumnWidth(0, 40 * 256);
			sheet.SetColumnWidth(1, 20 * 256);

			MemoryStream stream = new MemoryStream();
			workbook.Write(stream);

			return stream;

		}


	}
}
