import { Photo } from "./photo";

export interface RootObject {
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    dateBirth: Date;
    knownAs: string;
    created: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    lookingFor: string;
    interests: string;
    city: string;
    country: string;
    photos: Photo[];
}
