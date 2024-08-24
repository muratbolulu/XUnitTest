using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.App;

namespace XUnitTest.Test
{
    public class CalculatorTest
    {
        public Calculator _calculator { get; set; }
        private Mock<ICalculatorService> _myMock { get; set; }

        public CalculatorTest()
        {
            _myMock = new Mock<ICalculatorService>();//  ICalculatorService servisini impletente eden class ı taklit eder.
            _calculator = new Calculator(_myMock.Object); //constructor da nesne örneği alındığında mock araya girer ve fake yapı başlar.
        }

        //delege'ler metotları, event'lerde delegeleri işaret eder.

        //İsimlendirme;
        ////[MethodName_StateUnderTest_ExceptedBehavior]
        //add_simpleValues_returnTotalValue
        //Index_WhenIdIsNull_ReturnADirectToIndexHome
        //Microsoft'ta
        //IndexReturnADirectToIndexHomeWhenIdIsNull


        //Fact -->> attribute dür. Parametre almaz. Test edilecek metodlara verilir.VS tanır.
        //Theory  -->> parametre almak zorundadır. Parametre geçmek için InlineData.
        //InlineData  -->> parametreler alır. İstenildiği kadar peşpeşe kullanılabilir.
        //Arrenge -->> değişkenler initialize edilir. Hazırlık evresidir.
        //Act -->>davranış gösterimi, test edeceğimiz metodun çalışacağı yer.

        //Assert -->> çıkan sonucun doğru/yanlış olduğunu gösterir.


        //[Fact]
        public void AddTestEqual()
        {
            //Arrenge
            int a = 5;
            int b = 20;

            //Act
            var total = _calculator.Add(a, b);

            //Assert
            Assert.Equal<int>(25, total); //beklenen, metoddan gelen

            //Assert.NotEqual<int>(25, total); //aynı 2 değerse yanlış çalıştığını gösterir 
        }

        //[Fact]
        public void AddTestContain()
        {

            Assert.Contains("Murat", "Murat Bolulu"); //beklenen, metoddan gelen
            //Assert.Contains("Mustafa", "Murat Bolulu"); //beklenen, metoddan gelen

            //var names = new string[] { "Murat", "Mustafa", "Deniz" };
            var names2 = new List<string> { "Murat", "Mustafa", "Deniz" };

            Assert.Contains(names2, x => x == "Deniz"); //dizin içinde var mı yok mu kontrolüdür.
        }


        //[Fact]
        public void AddTestBoolean()
        {
            //Assert.True(5 < 6); // kullanılan true ise true bekler, false ise false bekler.
            //Assert.False(5 < 2); // kullanılan true ise true bekler, false ise false bekler.

            Assert.True("".GetType() == typeof(string));
        }

        //[Fact]
        public void AddTestMatch()
        {
            //regex example olarak aratırsak tarayıcı da 
            //içeriğde kelimeyi kontrol eder. 

            var regEx = "^dog";

            Assert.Matches(regEx, "dog Karabaş");
        }

        //[Fact]
        public void AddTestStartEndWith()
        {
            //StartWith Gelen ifadenin neyle başladığına bakar.
            //EndWith Gelen ifadenin neyle bittiğine bakar.

            //Assert.StartsWith("Bir", "Bir masal");
            //Assert.EndsWith("masal", "Bir masal");  //ok
            Assert.EndsWith("masal", "Bir masal olsun ");  //xx
        }

        //[Fact]
        public void AddTestEmpty()
        {
            //Empty içerideki class ın boş olmasına bakar. boş ise true
            //not Empty içerideki class ın boş olmasına bakar. boş değilse ise true

            //Assert.Empty(new List<string>());
            Assert.NotEmpty(new List<string>() { "Murat" });
        }

        //[Fact]
        public void AddTestRange()
        {
            //Range değeri aralığında ise true değilse false dir.

            Assert.InRange(10, 2, 20); //10 değeri 2 ile 20 arasında olduğundan true döner
            //Assert.NotInRange(10,2,20); //10 değeri 2 ile 20 arasında olduğundan false döner
        }

        //[Fact]
        public void AddTestSingle()
        {
            //verilen dizinin 1 elemanı varsa true değilse false döner

            //Assert.Single(new List<string>() { "Murat"}); // ok
            //Assert.Single(new List<string>() { "Murat", "Mustafa" }); // false
            Assert.Single<string>(new List<string>() { "Murat" }); // false  //tip güvenli kullanım.
        }

        //[Fact]
        public void AddTestIsType()
        {
            //alacağı değişkenin tipini kontrol eder. tip karşılaştırması yapar.ç

            //Assert.IsType<string>("Murat");
            //Assert.IsNotType<int>("Murat");
        }

        //[Fact]
        public void AddTestIsAssignableFrom()
        {
            //bir tipin bir tipe referans tiplerini kontrol eder. Önr: list in referansları arasında IEnumerable vardır.
            //kullanım örneği: bekledğim bir tipin bir interface'in mirası mı değil mi kontrolü gibi.

            Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());
            Assert.IsAssignableFrom<Object>("Murat");
        }

        //[Fact]
        public void AddTestNull()
        {
            //bir değerin null olup olmadığını kontrol eder.

            Assert.Null(null);
            //Assert.NotNull(5);
        }


        //[Theory]
        //[InlineData(5, 6, 11)]
        //[InlineData(45,2,24)]
        public void AddTestInlineData(int a, int b, int expectedTotal)
        {
            var actualTotal = _calculator.Add(a, b);

            Assert.Equal(expectedTotal, actualTotal);
        }

        //[Theory]
        //[InlineData(0, 6, 0)]
        //[InlineData(5, 0, 0)]
        public void AddTwo_ZeroValues_ReturnZeroValue(int a, int b, int expectedTotal)
        {
            var actualTotal = _calculator.AddTwo(a, b);

            Assert.Equal(expectedTotal, actualTotal);
        }


        //Moq
        //dönüş değerini -metottaki işlemleri yapmadan, service e girmeden- beklenen değeri (expectedValue) ile bitiriyor.

        //[Theory]
        //[InlineData(0, 6, 0)]
        //[InlineData(5, 0, 0)]
        public void AddTwo_ZeroValues_ReturnZeroValue2(int a, int b, int expectedTotal)
        {

            _myMock.Setup(x => x.AddTwo(a, b)).Returns(expectedTotal); //service e gitmez.
            Assert.Equal(expectedTotal, _calculator.AddTwo(a,b)); //service gitmeden retur expectedTotal kabul edilir.

            //var actualTotal = _calculator.AddTwo(a, b); //service gider.
            //Assert.Equal(expectedTotal, actualTotal);

        }


        //[Theory]
        //[InlineData(0, 6, 0)]
        //[InlineData(5, 0, 0)]
        public void AddTwo_ZeroValues_ReturnZeroValue3(int a, int b, int expectedTotal)
        {

            _myMock.Setup(x => x.AddTwo(a, b)).Returns(expectedTotal); //service e gitmez.
            Assert.Equal(expectedTotal, _calculator.AddTwo(a, b)); //service gitmeden retur expectedTotal kabul edilir.

            //_myMock.Verify(x=>x.AddTwo(a,b), Times.Once); //en az 1 kere çalışması
            //_myMock.Verify(x=>x.AddTwo(a,b), Times.Never); //hiç çalışmaması
            _myMock.Verify(x=>x.AddTwo(a,b), Times.AtLeast(2)); //AddTwo metodunun 2 kere çalışması beklenir.
        }


        //birden fazla kez Assert metodu kullanılabilir bir test içerisinde. tüm şartlara bakar
        //[Theory]
        //[InlineData(0, 6, 0)]
        public void AddTwo_ZeroValues_ReturnZeroValue4(int a, int b, int expectedTotal)
        {

            _myMock.Setup(x => x.AddTwo(a, b)).Returns(expectedTotal); 
            Assert.Equal(expectedTotal, _calculator.AddTwo(a, b)); //false
            Assert.NotEmpty(null);  //false
        }

        //throw ile bir hata fırlatmak için kullanılır.
        //örn: bir servisten dönen hata simule edilebilir.
        //[Theory]
        //[InlineData(0, 5)]
        public void Multip_ZeroValue_ReturnException(int a, int b)
        {

            _myMock.Setup(x=>x.Multip(a,b)).Throws(new Exception("a==0 olamaz.."));

            Exception exception = Assert.Throws<Exception>(()=>_calculator.Multip(a,b));

            Assert.Equal("a==0 olamaz.",exception.Message);
        }

        //callback() ve It.IsAny<type>()
        [Theory]
        [InlineData(3, 5, 15)]
        public void Multip_CallBack_ItIsAny_ReturnZeroValue5(int a, int b, int expectedTotal)
        {
            int actualMultip=0;
            _myMock.Setup(x => x.Multip(It.IsAny<int>(), It.IsAny<int>())).Callback<int,int>((x,y)=> actualMultip = x*y);

            Console.WriteLine(actualMultip);
            //inline data ile
            _calculator.Multip(a, b);
            Assert.Equal(expectedTotal, actualMultip);

            //manuel verilen data ile
            _calculator.Multip(5, 20);
            Assert.Equal(100, actualMultip);
        }

    }
}
