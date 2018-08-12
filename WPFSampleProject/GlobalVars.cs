using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSampleProject
{
    public static class GlobalVars
    {
        public static MainWindowViewModel ViewModel = new MainWindowViewModel();
    }

    public class Database {
        static readonly string PageCatalog = "QueryPages/";

        static readonly Uri db1_page1 = new Uri($"{PageCatalog + nameof(Db1_Page1)}.xaml", UriKind.Relative);
        static readonly Uri db1_page2 = new Uri($"{PageCatalog + nameof(Db1_Page2)}.xaml", UriKind.Relative);
        static readonly Uri db1_page3 = new Uri($"{PageCatalog + nameof(Db1_Page3)}.xaml", UriKind.Relative);

        static readonly Uri db2_page1 = new Uri($"{PageCatalog + nameof(Db2_Page1)}.xaml", UriKind.Relative);

        static readonly Uri db3_page1 = new Uri($"{PageCatalog + nameof(Db3_Page1)}.xaml", UriKind.Relative);

        static readonly Uri db4_page1 = new Uri($"{PageCatalog + nameof(Db4_Page1)}.xaml", UriKind.Relative);
        static readonly Uri db4_page2 = new Uri($"{PageCatalog + nameof(Db4_Page2)}.xaml", UriKind.Relative);

        static readonly Uri db5_page1 = new Uri($"{PageCatalog + nameof(Db5_Page1)}.xaml", UriKind.Relative);

        static readonly Uri db6_page1 = new Uri($"{PageCatalog + nameof(Db6_Page1)}.xaml", UriKind.Relative);
        static readonly Uri db6_page2 = new Uri($"{PageCatalog + nameof(Db6_Page2)}.xaml", UriKind.Relative);
        static readonly Uri db6_page3 = new Uri($"{PageCatalog + nameof(Db6_Page3)}.xaml", UriKind.Relative);

        public string Code { get; set; }

        public bool SpecAccess { get; set; } = false;

        public string Name {
            get {
                switch (Code) {
                    case "01": return "Database 1";
                    case "02": return "Database 2";
                    case "03": return "Database 3";
                    case "04": return "Database 4";
                    case "05": return "Database 5";
                    case "06": return "Database 6";
                    default: throw new Exception("Unknown database code.");
                }
            }
        }

        public List<Query> QueryList {
            get {
                switch (Code) {
                    case "01":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db1_page1 },
                            new Query { Name = "Query 2", Uri = db1_page2 },
                            new Query { Name = "Query 3", Uri = db1_page3 }
                        };
                    case "02":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db2_page1 }
                        };
                    case "03":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db3_page1 }
                        };
                    case "04":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db4_page1 },
                            new Query { Name = "Query 2", Uri = db4_page2 }
                        };
                    case "05":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db5_page1 }
                        };
                    case "06":
                        return new List<Query> {
                            new Query { Name = "Query 1", Uri = db6_page1 },
                            new Query { Name = "Query 2", Uri = db6_page2 },
                            new Query { Name = "Query 3", Uri = db6_page3 }
                        };
                    default:
                        throw new Exception("Unknown database code.");
                }
            }
        }
    }

    public class Query {
        public string Name { get; set; }

        public Uri Uri { get; set; }
    }
}
