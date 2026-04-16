using ScottPlot;

namespace ArtGestion.Helpers
{
    public static class ChartHelper
    {
        public static byte[] GenererGraphiqueBarres(List<string> labels, List<int> valeurs, string titre)
        {
            var plt = new Plot(800, 450);

            double[] values = valeurs.Select(x => (double)x).ToArray();
            double[] positions = Enumerable.Range(0, valeurs.Count).Select(x => (double)x).ToArray();

            var bar = plt.AddBar(values, positions);
            plt.Title(titre);
            plt.XTicks(positions, labels.ToArray());
            plt.SetAxisLimits(yMin: 0);
            plt.Grid();

            string tempFile = Path.GetTempFileName() + ".png";
            plt.SaveFig(tempFile);

            byte[] bytes = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);

            return bytes;
        }

        public static byte[] GenererGraphiqueCamembert(List<string> labels, List<int> valeurs, string titre)
        {
            var plt = new Plot(800, 450);

            double[] values = valeurs.Select(x => (double)x).ToArray();

            var pie = plt.AddPie(values);
            pie.SliceLabels = labels.ToArray();
            pie.ShowPercentages = true;
            pie.ShowLabels = true;

            plt.Title(titre);

            string tempFile = Path.GetTempFileName() + ".png";
            plt.SaveFig(tempFile);

            byte[] bytes = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);

            return bytes;
        }
    }
}