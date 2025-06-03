using System.Text.RegularExpressions;
using TFC.Application.DTO.Serialize_Deserialize;

namespace TFC.Transversal.GenericUtils
{
    public static class ExerciseProgressUtils
    {
        private const string Pattern = @"^(?<sets>\d+)x(?<reps>\d+)@(?<weight>\d+(\.\d+)?)$";

        public static string Serialize(int sets, int reps, decimal weight)
        {
            return $"{sets}x{reps}@{weight}";
        }

        public static DeserializeDTO? Deserialize(string progressString)
        {
            try
            {
                var match = Regex.Match(progressString, Pattern);
                if (!match.Success)
                    throw new FormatException($"Formato de progreso inválido: {progressString}");

                return new DeserializeDTO
                {
                    Set = int.Parse(match.Groups["sets"].Value),
                    Reps = int.Parse(match.Groups["reps"].Value),
                    Weight = decimal.Parse(match.Groups["weight"].Value)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool TryDeserialize(string progressString, out int sets, out int reps, out decimal weight)
        {
            sets = reps = 0;
            weight = 0m;

            var match = Regex.Match(progressString, Pattern);
            if (!match.Success) return false;

            return int.TryParse(match.Groups["sets"].Value, out sets) &&
                   int.TryParse(match.Groups["reps"].Value, out reps) &&
                   decimal.TryParse(match.Groups["weight"].Value, out weight);
        }
    }
}