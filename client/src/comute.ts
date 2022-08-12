export interface Comute {

    id: number;
    departureTime: Date;
    expectedArrival: Date;
    origin: string;
    days: number;
    destination: string;
    availableSeats: number;
    owner: string;
    notes: string;
    addedOn: Date;
    
}