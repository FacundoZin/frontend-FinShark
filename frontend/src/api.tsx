import axios from "axios";
import { CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./companydates";



interface SearchResponse {
    data: CompanySearch[];
}

export const searchCompanies = async (search_of_user: string) =>{

    try {

        const apiKey = import.meta.env.VITE_APP_API_KEY; // Accede a la API key desde las variables de entorno

        if (!apiKey) {
            throw new Error("La API key no está definida. Verificá tu archivo .env");
        }

        const data = await axios.get<SearchResponse>(`https://financialmodelingprep.com/api/v3/search?query=${search_of_user}&limit=10&exchange=NASDAQ&apikey=${apiKey}`);

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

export const Get_company_info = async (symbol_of_company: string) => {

    try{

        const apikey = import.meta.env.VITE_APP_API_KEY;
        if (!apikey) {
            console.log("La API key no está definida. Verificá tu archivo .env");
        }

        const company_information = axios.get<CompanyProfile[]>(`https://financialmodelingprep.com/api/v3/profile/${symbol_of_company}?apikey=${apikey}`);

        return company_information;
    
    }
    catch(error){
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

export const GetCompanyKeymetrics = async (query: string) => {

    try{

        const apikey = import.meta.env.VITE_APP_API_KEY;
        if (!apikey) {
            console.log("La API key no está definida. Verificá tu archivo .env");
            return null
        }

        const KeyMetrics = axios.get<CompanyKeyMetrics[]>(`https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=${apikey}`);

        return KeyMetrics;
    
    }
    catch(error){
        if (axios.isAxiosError(error)) {

            if (error.response) { 
                console.log("Error de respuesta del servidor:", error.response);
                return null
            } else if (error.request) {
                console.log("No se recibió respuesta del servidor:", error.request);
                return null
            } else {
                console.log("Error en la configuración de la solicitud:", error.message);
                return null
            }

        }
        else{
            console.log("Error desconocido:", error);
            return null
        }
    }
}

export const GetIncomeStatement = async (query: string) => {

    try{

        const apikey = import.meta.env.VITE_APP_API_KEY;
        if (!apikey) {
            console.log("La API key no está definida. Verificá tu archivo .env");
            return null
        }

        const IncomeStatement = axios.get<CompanyIncomeStatement[]>(`https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=40&apikey=${apikey}`);

        return IncomeStatement;
    
    }
    catch(error){
        if (axios.isAxiosError(error)) {

            if (error.response) { 
                console.log("Error de respuesta del servidor:", error.response);
                return null
            } else if (error.request) {
                console.log("No se recibió respuesta del servidor:", error.request);
                return null
            } else {
                console.log("Error en la configuración de la solicitud:", error.message);
                return null
            }

        }
        else{
            console.log("Error desconocido:", error);
            return null
        }
    }

};


