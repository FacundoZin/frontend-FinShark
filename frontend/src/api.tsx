import axios from "axios";
import { CompanySearch } from "./companydates";
import { isAxiosError } from "axios";
import { AxiosError } from "axios";

interface SearchResponse {
    data: CompanySearch[];
}

export const searchCompanies = async (query: string) =>{
    try {
        const apiKey = import.meta.env.VITE_API_KEY; // Accede a la API key desde las variables de entorno
        if (!apiKey) {
            throw new Error("La API key no está definida. Verificá tu archivo .env");
        }

        const data = await axios.get<SearchResponse>(
            `https://financialmodelingprep.com/api/v3/search-ticker?query=${query}AA&limit=10&exchange=NASDAQ&apikey=${apiKey}`
        );

        return data;
    } catch (error) {

        if(isAxiosError(error) == true){
            const axiosError = error as AxiosError;
            console.log("Error al buscar las empresas:", axiosError.message);
        }
        else{
            console.log("Error desconocido:", error);
        }
        throw error;
    }
}