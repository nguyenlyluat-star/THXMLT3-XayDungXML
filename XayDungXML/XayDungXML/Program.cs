using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Hỗ trợ hiển thị tiếng Việt trên cửa sổ Console
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1 & 2. Xây dựng cây XML dựa theo sơ đồ và thêm các phần tử con
        XDocument doc = new XDocument(
            new XElement("bookstore",
                // Cuốn sách thứ nhất (Khớp với sơ đồ đề bài)
                new XElement("book",
                    new XAttribute("lang", "en"),
                    new XAttribute("category", "COOKING"),
                    new XElement("title", "Everyday Italian"),
                    new XElement("author", "Giada De Laurentiis"),
                    new XElement("year", "2005"),
                    new XElement("price", "30.00")
                ),
                // Thêm các phần tử con tiếp theo (cuốn sách thứ hai)
                new XElement("book",
                    new XAttribute("lang", "en"),
                    new XAttribute("category", "CHILDREN"),
                    new XElement("title", "Harry Potter"),
                    new XAttribute("author", "J.K. Rowling"),
                    new XElement("year", "2005"),
                    new XElement("price", "19.99")
                ),
                // Thêm phần tử con thứ ba để dễ thao tác tìm min/max giá
                new XElement("book",
                    new XAttribute("lang", "en"),
                    new XAttribute("category", "WEB"),
                    new XElement("title", "Learning XML"),
                    new XElement("author", "Erik T. Ray"),
                    new XElement("year", "2003"),
                    new XElement("price", "39.95")
                )
            )
        );

        // Xuất (export) cây XML ra file có tên "bookstore.xml"
        string filePath = "bookstore.xml";
        doc.Save(filePath);
        Console.WriteLine($"Đã xây dựng và export thành công file XML tại: {filePath}\n");

        // 3. Hiển thị các thông tin yêu cầu:
        XElement root = doc.Root;

        // a. Phần tử gốc (Root element)
        Console.WriteLine($"a. Phần tử gốc: <{root?.Name}>");

        // b. Số phần tử con và cháu
        int directChildren = root?.Elements().Count() ?? 0;
        int totalDescendants = root?.Descendants().Count() ?? 0;
        Console.WriteLine($"b. Số phần tử con trực tiếp: {directChildren}");
        Console.WriteLine($"   Tổng số phần tử con, cháu (Descendants): {totalDescendants}");

        // c. Tìm phần tử có giá thấp nhất và cao nhất
        var books = root?.Elements("book");
        if (books != null && books.Any())
        {
            var minBook = books.OrderBy(b => decimal.Parse(b.Element("price")?.Value ?? "0")).First();
            var maxBook = books.OrderByDescending(b => decimal.Parse(b.Element("price")?.Value ?? "0")).First();

            Console.WriteLine("\nc. Thông tin phần tử sách có giá thấp nhất và cao nhất:");
            Console.WriteLine("   -> Sách có giá thấp nhất:");
            Console.WriteLine($"      + Tựa đề: {minBook.Element("title")?.Value}");
            Console.WriteLine($"      + Giá: {minBook.Element("price")?.Value}");

            Console.WriteLine("   -> Sách có giá cao nhất:");
            Console.WriteLine($"      + Tựa đề: {maxBook.Element("title")?.Value}");
            Console.WriteLine($"      + Giá: {maxBook.Element("price")?.Value}");
        }
    }
}