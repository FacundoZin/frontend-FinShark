import axios from "axios";
import { CompanySearch } from "./companydates";



interface SearchResponse {
    data: CompanySearch[];
}

export const searchCompanies = async (query: string) =>{

    try {

        const apiKey = import.meta.env.VITE_APP_API_KEY; // Accede a la API key desde las variables de entorno
        if (!apiKey) {
            throw new Error("La API key no está definida. Verificá tu archivo .env");
        }

        const data = await axios.get<SearchResponse>(
            `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${apiKey}`
            
        );

        return data;

    } catch (error) {
        
        if (axios.isAxiosError(error)) {

            if (error.response) { 
                console.log("Error de respuesta del servidor:", error.response);
            } else if (error.request) {
                
                console.log("No se recibió respuesta del servidor:", error.request);
            } else {
                
                console.log("Error en la configuración de la solicitud:", error.message);
            }

        }
        else{

            console.log("Error desconocido:", error);
        
        }
    }
}

