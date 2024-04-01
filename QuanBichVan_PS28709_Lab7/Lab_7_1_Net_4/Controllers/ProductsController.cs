using Lab_7_1_Net_4.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Lab_7_1_Net_4.Controllers
{
    public class ProductsController: Controller
    {
        private readonly InventoryContext _context;

        public ProductsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
            // lấy danh sách các sản phẩm từ cơ sở dữ liệu và truyền chúng cho view.
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // sử dụng để hiển thị thông tin chi tiết của một sản phẩm dựa trên id được cung cấp
            // kiểm tra xem id có null hay không, nếu _context.Products là null, điều này có thể là do không thể kết nối đến cơ sở dữ liệu hoặc có lỗi xảy ra khi truy vấn dữ liệu
            if (id == null || _context.Products == null)
            {
                // nếu null sẽ trả về notfound để hiển thị trang 404.
                return NotFound();
            }
            // trường hợp không null
            // truy vấn LINQ để lấy thông tin của một sản phẩm từ cơ sở dữ liệu dựa trên ProductId đã được cung cấp.
            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            //sử dụng toán tử ba ngôi để quyết định xem liệu sản phẩm đã được tìm thấy từ cơ sở dữ liệu hay không.
            //Nếu sản phẩm không tồn tại (tức là product là null), phương thức sẽ trả về kết quả NotFound(). Ngược lại, nếu sản phẩm được tìm thấy (tức là product không null), phương thức sẽ trả về một view View(product) để hiển thị thông tin chi tiết của sản phẩm.
            return product == null ? NotFound() : View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        
        // [HttpPost] chỉ ra rằng phương thức này chỉ được gọi khi có một yêu cầu HTTP POST gửi đến action method từ trình duyệt.
        [HttpPost]

        // [ValidateAntiForgeryToken] được sử dụng để bảo vệ ứng dụng khỏi các tấn công Cross-Site Request Forgery(CSRF). Nó yêu cầu một mã thông báo được tạo từ server(XSRF/CSRF token) được gửi kèm với yêu cầu, và server sẽ kiểm tra tính hợp lệ của mã thông báo này.
        [ValidateAntiForgeryToken]
        //Đây là một attribute (thuộc tính) được sử dụng để chỉ định các thuộc tính của đối tượng Product mà phương thức sẽ chấp nhận từ dữ liệu đầu vào. Các thuộc tính được liệt kê (ProductId, Name, Category, Color, UnitPrice, AvailableQuantity, CreatedDate) sẽ được sử dụng từ dữ liệu đầu vào để tạo mới sản phẩm. Điều này giúp ngăn chặn việc chấp nhận các thuộc tính không mong muốn từ dữ liệu người dùng và bảo vệ ứng dụng khỏi các cuộc tấn công gửi dữ liệu không mong muốn (binding attacks).
        public async Task<IActionResult> Create([Bind("ProductId,Name,Category,Color,UnitPrice,AvailableQuantity,CreatedDate")] Product product)
        {
            // kiểm tra xem modelState có hợp lệ hay không
            // ModelState là một tập hợp các giá trị trạng thái và lỗi liên quan đến việc binding dữ liệu và xác thực dữ liệu trong quá trình POST. ModelState.IsValid sẽ trả về true nếu không có lỗi nào xảy ra trong quá trình binding và xác thực, tức là dữ liệu người dùng đã được nhập đúng và hợp lệ.
            if (ModelState.IsValid) 
            {
                //nếu hợp lệ thì add vào
                _context.Add(product);
                // lưu dữ kiện sau khi add
                await _context.SaveChangesAsync();
                // chuyển hướng người dùng đến trang hiển thị danh sách các sản phẩm
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        //tương tự như hàm create phía trên
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            return product == null ? NotFound() : View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Tương tự như Create phía trên, nhưng cho phép chỉnh sửa một sản phẩm đã tồn tại.
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Category,Color,UnitPrice,AvailableQuantity,CreatedDate")] Product product)
        {
            // nếu id cung cấp khác với id của sản phẩm (productId)
            if(id != product.ProductId)
            {
                // trả về trang 404
                return NotFound();
            }
            // kiểm tra xem modelState có hợp lệ hay không
            if (ModelState.IsValid)
            {
                // đoạn này bắt ngoại lệ
                try
                {
                    // cập nhật thông tin của sản phẩm trong cơ sở dữ liệu
                    _context.Update(product);
                    // sau đó lưu
                    await _context.SaveChangesAsync();
                }
                // Nếu có ngoại lệ xảy ra trong quá trình lưu thay đổi vào cơ sở dữ liệu,  nó sẽ được bắt và xử lý bởi khối catch này.
                catch (DbUpdateConcurrencyException)
                {
                    // Trong trường hợp ngoại lệ xảy ra do không tìm thấy sản phẩm có ProductId tương ứng
                    // phương thức kiểm tra xem sản phẩm có tồn tại không ProductExists(product.ProductId) (check ở dưới cùng)
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Nếu chỉnh sửa thành công và lưu thay đổi vào cơ sở dữ liệu, phương thức chuyển hướng người dùng đến trang danh sách sản phẩm 
                return RedirectToAction(nameof(Index));
            }
            // Nếu dữ liệu nhập vào từ form không hợp lệ, phương thức trả về lại view cùng với đối tượng product mà người dùng đã nhập. Hiển thị lại form chỉnh sửa sản phẩm với thông báo lỗi để người dùng có thể sửa đổi và gửi lại dữ liệu
            return View(product);
        }

        // GET: Products/Delete/5
        // xóa một sản phẩm từ cơ sở dữ liệu. Delete để hiển thị xác nhận trước khi xóa, và DeleteConfirmed xóa thực sự sản phẩm khỏi cơ sở dữ liệu sau khi xác nhận.
        public async Task<IActionResult> Delete(int? id)
        {
            // nếu không có id như vậy thì trả về trang 404
            if(id == null || _context.Products == null)
            {
                return NotFound();
            }
            //nếu có sản phẩm được tìm thấy, phương thức trả về view Delete với đối tượng product để hiển thị thông tin của sản phẩm trong giao diện xác nhận xóa.
            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            return product == null ? NotFound() : View(product);
        }

        // POST: Products/Delete/5
        // một attribute được sử dụng để chỉ định rằng phương thức này sẽ xử lý yêu cầu POST và có tên hành động (action name) là "Delete". Điều này giúp xác định rằng phương thức này là phương thức xác nhận xóa một sản phẩm.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Dòng này kiểm tra xem _context.Products có khởi tạo hay không
            if (_context.Products == null)
            {
                //trả về một kết quả Problem với thông báo lỗi
                return Problem("Entity set 'InventoryContext.Products'  is null.");
            }
            // sử dụng phương thức FindAsync để tìm kiếm sản phẩm trong cơ sở dữ liệu dựa trên ProductId được cung cấp
            var product = await _context.Products.FindAsync(id);
            // kiểm tra nếu tìm thấy sản phẩm đó thì sản phẩm đó sẽ được loại bỏ (xóa) khỏi DbSet 
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            // lưu lại thay đổi
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //phương thức kiểm tra xem sản phẩm có tồn tại không
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
