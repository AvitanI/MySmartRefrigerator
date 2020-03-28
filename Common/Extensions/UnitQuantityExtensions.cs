using Common.Enumerations;

namespace Common.Extensions
{
    /// <summary>
    /// Represent extensions for unit of quantity
    /// </summary>
    public static class UnitQuantityExtensions
    {
        #region Consts

        private static readonly string LITER = "ליטרים";
        private static readonly string MILILITER = "מיליליטרים";
        private static readonly string KILOGRAM = "קילוגרמים";
        private static readonly string GRAM = "גרמים";
        private static readonly string MILIGRAM = "מיליגרמים";

        #endregion

        #region Extensions

        /// <summary>
        /// Convert string unit of quantity to internal enumeration <see cref="EUnitQuantity"/>
        /// </summary>
        /// <param name="unitQuantity">The unit of quantity</param>
        /// <returns></returns>
        public static EUnitQuantity ToUnitQuantity(this string unitQuantity)
        {
            // Check for empty value
            if (string.IsNullOrWhiteSpace(unitQuantity))
            {
                return EUnitQuantity.None;
            }

            unitQuantity = unitQuantity.Trim();

            if (unitQuantity == LITER)
            {
                return EUnitQuantity.Liter;
            }
            else if (unitQuantity == MILILITER)
            {
                return EUnitQuantity.Mililiter;
            }
            else if (unitQuantity == KILOGRAM)
            {
                return EUnitQuantity.Kilogram;
            }
            else if (unitQuantity == GRAM)
            {
                return EUnitQuantity.Gram;
            }
            else if (unitQuantity == MILIGRAM)
            {
                return EUnitQuantity.Miligram;
            }
            else
            {
                return EUnitQuantity.None;
            }
        }

        #endregion
    }
}
