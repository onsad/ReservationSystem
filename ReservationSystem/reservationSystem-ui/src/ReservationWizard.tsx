import { useState } from "react";
import {
    Box,
    Button,
    Step,
    StepLabel,
    Stepper,
    TextField,
    Alert
} from "@mui/material";
import { createReservation } from "./api/reservations";

const steps = ["Welcome", "Reservation", "Done"];

export default function ReservationWizard() {
    const [activeStep, setActiveStep] = useState(0);

    const [email, setEmail] = useState("");
    const [phone, setPhone] = useState("");
    const [tickets, setTickets] = useState(1);

    const [error, setError] = useState<string | null>(null);
    const [code, setCode] = useState<string | null>(null);
    const [errors, setErrors] = useState<any>({});

    const handleNext = async () => {
        setError(null);
        setErrors({});

        if (activeStep === 1) {
            try {
                const res = await createReservation({
                    email,
                    phone,
                    tickets,
                });

                setCode(res.code);

                setEmail("");
                setPhone("");
                setTickets(1);

            } catch (e: any) {
                if (e.errors) {
                    setErrors(e.errors);
                } else {
                    setError("Chyba při vytváření rezervace");
                }

                return;
            }
        }
        
        setActiveStep((s) => s + 1);
    };

    return (
        <Box sx={{ maxWidth: 400, mx: "auto", mt: 4 }}>
            <Stepper activeStep={activeStep}>
                {steps.map((s) => (
                    <Step key={s}>
                        <StepLabel>{s}</StepLabel>
                    </Step>
                ))}
            </Stepper>

            <Box mt={4}>
                {error && <Alert severity="error">{error}</Alert>}

                {activeStep === 0 && <div>Vítejte 👋</div>}

                {activeStep === 1 && (
                    <Box display="flex" flexDirection="column" gap={2}>
                        <TextField
                            label="Email"
                            value={email}
                            onChange={(e) => {
                                setEmail(e.target.value);
                                setErrors((prev: any) => ({ ...prev, Email: null }));
                            }}
                            error={!!errors.Email}
                            helperText={errors.Email?.[0]}
                            fullWidth
                        />

                        <TextField
                            label="Telefon"
                            value={phone}
                            onChange={(e) => {
                                setPhone(e.target.value);
                                setErrors((prev: any) => ({ ...prev, Phone: null }));
                            }}
                            error={!!errors.Phone}
                            helperText={errors.Phone?.[0]}
                            fullWidth
                        />

                        <TextField
                            label="Počet lístků"
                            type="number"
                            value={tickets}
                            onChange={(e) => {
                                setTickets(Number(e.target.value));
                                setErrors((prev: any) => ({ ...prev, Tickets: null }));
                            }}
                            error={!!errors.Tickets}
                            helperText={errors.Tickets?.[0]}
                            fullWidth
                        />
                    </Box>
                )}

                {activeStep === 2 && (
                    <Box>
                        <Alert severity="success">
                            Rezervace vytvořena ✅
                        </Alert>

                        <div>Kód rezervace:</div>
                        <strong>{code}</strong>
                    </Box>
                )}
            </Box>

            <Box mt={2}>
                <Button
                    disabled={activeStep === 0}
                    onClick={() => setActiveStep((s) => s - 1)}
                >
                    Zpět
                </Button>

                {activeStep < 2 && (
                    <Button variant="contained" onClick={handleNext}>
                        Další
                    </Button>
                )}
            </Box>
        </Box>
    );
} 