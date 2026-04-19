export class BaseService {
    constructor(debugParam = false) {
        this.validateResponse = (response) => {
            // console.log(response);
            if (!response.ok) {
                const status = `${response.status} - ${response.statusText}`;
                throw Error(status);
            }
            return response;
        };
        this.readResponseAsText = (response) => {
            // console.log(response);
            return response.text();
        };
        this.logError = (error) => {
            // console.log("Issue w/ fetch call: \n", error);
        };
        this.getToken = () => {
            const token = document.querySelector('input[name="__RequestVerificationToken"]');
            return (token) ? token.getAttribute("value") : "";
        };
        this.debug = debugParam;
    }
    readResponseAsJson(response) {
        // console.log(response);
        return response.json();
    }
}
