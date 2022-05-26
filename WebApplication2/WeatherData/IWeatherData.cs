using WebApplication2.Models;

namespace WebApplication2.WeatherData
{
    public interface IWeatherData
    {
        /*toate prognozele*/
        List<Weather> getWeathers();

        /*prognoza cu un anumit id  */
        Weather GetWeather(int Id); 

        /*adauga o prognoza data ca parametru*/
        Weather AddWeather(Weather weather);

        /*sterge o prognoza dupa id*/
        void DeleteWeather(int Id); 

        /*editeaza o prognoza, avand obiectul nou modificat si id-ul obiectului de editat*/
        Weather EditWeather(int Id, Weather weather);

        /*toate prognozele dintr-un anumit interval de timp */
        List<Weather> GetWeathersBetweenDates(DateTime date1, DateTime date2);

        /*toate prognozele dintr-o anumita zi*/
        List<Weather> GetWeathersFromDay(DateTime date_day);

        /*sterge toate prognozele mai vechi de 30 de zile sau de dupa 30 de zile de la ziua curenta*/
        List<Weather> GetWeathersNotTooFar(DateTime today);

        /* editeaza o prognoza, avand obiectul nou modificat si data si sursa pentru prognoza respectiva*/
       Weather EditWeatherFromDayFromSource(DateTime date, Source source, Weather weather);


        /* adauga o prognoza pentru o anumita zi urmatoare si arunca eroare daca ziua este invalida*/
       Weather AddWeatherWithDate(DateTime date, Weather weather);



    }
}
