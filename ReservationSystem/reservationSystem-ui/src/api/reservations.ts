    const BASE_URL = "https://localhost:7215/api/reservations";

export async function createReservation(data: {
    email: string;
    phone: string;
    tickets: number;
}) {
    const res = await fetch(BASE_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });

    if (!res.ok) {
        const err = await res.json();
        throw err;
    }

    return res.json();
}

export async function getReservation(code: string) {
    const res = await fetch(`${BASE_URL}/${code}`);
    if (!res.ok) throw new Error("Not found");
    return res.json();
}

export async function updateReservation(code: string, data: any) {
    const res = await fetch(`${BASE_URL}/${code}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    });

    if (!res.ok) throw await res.json();
    return res.json();
}

export async function deleteReservation(code: string) {
    const res = await fetch(`${BASE_URL}/${code}`, {
        method: "DELETE",
    });

    if (!res.ok) throw new Error("Delete failed");
} 