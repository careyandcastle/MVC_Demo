[NotMapped]
public class 盤點明細顯示ViewModel : 庫存盤點明細
{
    [DisplayName("商品名稱")]
    public string 商品名稱 { get; set; }

    [DisplayName("商品規格")]
    public string 商品規格 { get; set; }

    [DisplayName("單位")]
    public string 商品單位 { get; set; }

    [DisplayName("庫存結存量")]
    public decimal? 庫存結存量 { get; set; }

    [DisplayName("盤點數量")]
    public decimal 盤點數量顯示用 => this.盤點數量;
}
