export * from './pokemon.service';
import { PokemonService } from './pokemon.service';
export * from './weatherForecast.service';
import { WeatherForecastService } from './weatherForecast.service';
export const APIS = [PokemonService, WeatherForecastService];
