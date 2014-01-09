class DebugDataColumn
{
  public static void View(DataTable tbl)
  {
    DataColumnCollection cols = tbl.Columns;

    string format = "{0,-15}{1,-15} {2,-6}{3,-6}{4,-6}{5,-6}{6,-6}{7,-6}{8}";
    Console.WriteLine(format,
      "ColumnName", "DataType", "Uniqu", "ReadO", "AutoI",
      "DBNul", "MaxLe", "Defau", "Expre");

    foreach (DataColumn col in cols) {
      Console.WriteLine(format,
        col.ColumnName,
        col.DataType,
        col.Unique,
        col.ReadOnly,
        col.AutoIncrement,
        col.AllowDBNull,
        col.MaxLength,
        col.DefaultValue,
         col.Expression
       );
    }

    foreach (DataRow row in tbl.Rows) {
      for (int i = 0; i < tbl.Columns.Count; i++) {
        Console.Write("{0}\t", row[i]);
      }
      Console.WriteLine();
      break;
    }

    PrimaryKeys(tbl);
    Console.WriteLine("ChildRelations: {0}", tbl.ChildRelations.Count);
    Console.WriteLine("ParentRelations: {0}", tbl.ParentRelations.Count);
    Console.WriteLine("---------------------------");
  }

  /// <summary>
  /// 主キー情報の表示
  /// </summary>
  /// <param name="tbl">DataTable</param>
  private static void PrimaryKeys(DataTable tbl)
  {
    Console.WriteLine("主キー: {0}個", tbl.PrimaryKey.Length);
    DataColumn[] columns = tbl.PrimaryKey;

    for (int i = 0; i < columns.Length; i++)
      Console.Write("({0}) {1}: {2}\t",i,columns[i].ColumnName, columns[i].DataType);
    Console.WriteLine();
  }
}
