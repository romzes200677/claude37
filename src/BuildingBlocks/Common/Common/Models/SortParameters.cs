namespace Common.Models
{
    /// <summary>
    /// Параметры сортировки
    /// </summary>
    public class SortParameters
    {
        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string SortBy { get; set; } = "Id";

        /// <summary>
        /// Направление сортировки (true - по возрастанию, false - по убыванию)
        /// </summary>
        public bool Ascending { get; set; } = true;
    }
}