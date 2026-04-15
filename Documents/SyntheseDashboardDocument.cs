using ArtGestion.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ArtGestion.Documents
{
    public class SyntheseDashboardDocument : IDocument
    {
        private readonly DashboardViewModel _model;

        public SyntheseDashboardDocument(DashboardViewModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Column(column =>
                {
                    column.Item().Text("ART - Synthèse de gestion").Bold().FontSize(18);
                    column.Item().Text($"Date d'édition : {DateTime.Now:dd/MM/yyyy HH:mm}");
                });

                page.Content().Column(column =>
                {
                    column.Spacing(12);

                    column.Item().Text("Indicateurs clés").Bold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(100);
                        });

                        void Row(string libelle, string valeur)
                        {
                            table.Cell().Border(1).Padding(5).Text(libelle);
                            table.Cell().Border(1).Padding(5).Text(valeur);
                        }

                        Row("Exploitants", _model.TotalExploitants.ToString());
                        Row("Total titres", _model.TotalTitres.ToString());
                        Row("Titres actifs", _model.Actifs.ToString());
                        Row("Titres bientôt expirés", _model.BientotExpires.ToString());
                        Row("Titres expirés", _model.Expires.ToString());
                        Row("Alertes non lues", _model.AlertesNonLues.ToString());
                    });

                    column.Item().PaddingTop(10).Text("Répartition par région").Bold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                        });

                        table.Cell().Border(1).Padding(5).Text("Région").Bold();
                        table.Cell().Border(1).Padding(5).Text("Nombre").Bold();

                        for (int i = 0; i < _model.RegionsLabels.Count; i++)
                        {
                            table.Cell().Border(1).Padding(5).Text(_model.RegionsLabels[i]);
                            table.Cell().Border(1).Padding(5).Text(_model.RegionsCounts[i].ToString());
                        }
                    });

                    column.Item().PaddingTop(10).Text("Répartition par type de titre").Bold().FontSize(14);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                        });

                        table.Cell().Border(1).Padding(5).Text("Type").Bold();
                        table.Cell().Border(1).Padding(5).Text("Nombre").Bold();

                        for (int i = 0; i < _model.TypesTitreLabels.Count; i++)
                        {
                            table.Cell().Border(1).Padding(5).Text(_model.TypesTitreLabels[i]);
                            table.Cell().Border(1).Padding(5).Text(_model.TypesTitreCounts[i].ToString());
                        }
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
            });
        }
    }
}