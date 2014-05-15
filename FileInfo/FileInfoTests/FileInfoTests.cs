using System.Linq;
using FileInfoBussiness;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FileInfoTests
{
    [TestFixture]
    public class FileInfoTests
    {
        private FileInfoFinder service;
        private Mock<FileInfoRepository> repository;

        [SetUp]
        public void SetUp()
        {
            service = new FileInfoFinder();
            repository = new Mock<FileInfoRepository>();
            service.Repository = repository.Object;
        }

        [Test]
        public void RetrieveEmptyContent()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns(string.Empty);

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(0);
        }

        [Test]
        public void RetrieveOneKeyOnce()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns("Quijote");

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(1);
            result.Keys.First().Key.Should().Be("Quijote");
            result.Keys.First().Value.Should().Be(1);
        }

        [Test]
        public void RetrieveTwoKeys()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns("Quijote Sancho");

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(2);
            result.Keys.First().Key.Should().Be("Quijote");
            result.Keys.First().Value.Should().Be(1);
            result.Keys.Last().Key.Should().Be("Sancho");
            result.Keys.Last().Value.Should().Be(1);
        }

        [Test]
        public void RetrieveKeysWithCorrectNumberOfOccurrences()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns(GetFlatText(1));

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(192);
        }

        [Test]
        public void RetrieveRelevanceKeys()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns(GetFlatText(1));

            var result = service.Analize("pattern");

            result.Keys.ContainsKey("y").Should().BeFalse();
            result.Keys.ContainsKey("de").Should().BeFalse();
            result.Keys.ContainsKey("el").Should().BeFalse();
            result.Keys.ContainsKey("a").Should().BeFalse();
            result.Keys.ContainsKey("los").Should().BeFalse();
            result.Keys.ContainsKey("la").Should().BeFalse();
            result.Keys.ContainsKey("un").Should().BeFalse();
            result.Keys.ContainsKey("su").Should().BeFalse();
        }

        [Test]
        public void RetrieveGaplessKeys()
        {
            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns(" Quijote Sancho ");

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(2);
            result.Keys.First().Key.Should().Be("Quijote");
            result.Keys.First().Value.Should().Be(1);
            result.Keys.Last().Key.Should().Be("Sancho");
            result.Keys.Last().Value.Should().Be(1);
        }

        private string GetFlatText(int index)
        {
            if (index == 1)
            return
                @"TENIS Victoria sufrida del tenista de Manacor Nadal también suda ante Simon
                Tardó tres horas y 18 minutos en deshacerse del talentoso jugador francés
                El próximo rival del balear será el ruso Mikhail Youzhny
                El español Rafael Nadal accedió a los octavos de final del Abierto de Italia 
                tras derrotar en segunda ronda al francés Gilles Simon, por 7-6 (1), 6-7 (4) y 
                6-2, en 3 horas y 18 minutos. El próximo rival de Nadal, actual número uno del 
                ránking ATP, será el ruso Mikhail Youzhny, que derrotó en su partido de la segunda 
                ronda al kazajo Andrey Golubev, retirado por lesión en el segundo set cuando el 
                marcador era de 7-5 y 4-1 a favor de Youzhny. El partido entre Nadal y Simon, número 30 
                del mundo, cerró la jornada en la pista central del Foro Itálico de Roma, donde las 
                fuertes rachas de viento que en ella soplaron durante toda la jornada se moderaron a la 
                hora de disputarse este encuentro. El marcador final del partido fue un fiel reflejo de 
                lo que pudo verse sobre la arcilla, a un peleón Simon que, si bien no llegó a poner 
                completamente contra las cuerdas al español, jugó de tú a tú al número uno del mundo 
                durante todo el partido. Ambos tenistas consiguieron romper el servicio de su rival en 
                dos ocasiones en la primera manga, lo que derivó en un desenlace por desempate, del que 
                Nadal salió vencedor. En el segundo set pareció que Nadal iba a desmarcarse pronto de su 
                rival tras sumar tres juegos seguidos, pero Simon devolvió la moneda al español y empató 
                a tres la manga. La resolución del segundo envite volvió a ser de nuevo en el desempate, 
                esta vez favorable al francés, después de que Nadal desaprovechase una pelota de partido 
                con 6-5. En el último set, Nadal solventó el duelo dando el todo por el todo. Una rotura 
                del español para poner el 3-2 a su favor en el marcador fue el punto de inflexión que 
                finalmente decantó el partido de su lado. Gracias a esta sufrida y trabajada victoria, 
                Nadal podrá continuar defendiendo título en Roma, donde se proclamó campeón hace un año 
                al derrotar en la final al suizo Roger Federer, que hoy cayó eliminado en su debut frente 
                al francés Jeremy Chardy";
            return string.Empty;
        }
    }
}
