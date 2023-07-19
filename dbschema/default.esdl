module default {
    scalar type State extending enum<Mr, Mrs, Miss, Dr, Prof>;
    type Contact {
        required first_name: str;
        required last_name: str;
        required email: str;
        required title: State;
        required description: str;
        required date_of_birth: datetime;
        required marriage_status: bool;
    }
}
