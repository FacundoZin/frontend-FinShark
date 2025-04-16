import axios from "axios"
import { toast } from "react-toastify";

export const HandleError = (error : any) => {
    if(axios.isAxiosError(error)){
        var ErrorContent = error.response;
        if(Array.isArray(ErrorContent?.data.errors)){
            for(let val of ErrorContent.data.errors){
                toast.warning(val.description);
            }
        }else if (typeof ErrorContent?.data.errors === "object"){
            for(let e in ErrorContent.data.errors){
                const errorValue = ErrorContent.data.errors[e];
        
                if (Array.isArray(errorValue)) {
                    toast.warning(errorValue[0]);
                } else {
                    toast.warning(errorValue); 
                }
            }
        }
        else if (ErrorContent?.data){
            toast.warning(ErrorContent.data);
        }
        else if (ErrorContent?.status == 401){
            toast.warning("please login")
            window.history.pushState({}, "LoginPage", "/login");
        }
        else if (ErrorContent){
            toast.warning(ErrorContent.data);
        }
    }
}