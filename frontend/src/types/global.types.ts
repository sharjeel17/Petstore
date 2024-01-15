export interface IProduct {
    id: string,
    name: string,
    animal: string,
    breed: string,
    imageSrc: string | ArrayBuffer |null,
    imageFile?: File | null
    createdAt: string,
    updatedAt: string
}

export type AuthContextValueType = {
    token: string | null,
    setToken: (newToken: string) => void
}