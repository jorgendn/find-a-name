import { useGetNames, useLogout } from '../../hooks/authentication-hooks';

function Authenticated() {
    const { data: names } = useGetNames();

    console.log(names);

    const logoutQuery = useLogout();

    return (
        <>
        <ul>
            {names?.map(name => (<li>{name.name}</li>))}
        </ul>
                       <button
                    onClick={() => {
                        logoutQuery.mutate();
                    }}
                >
                    Log out
                </button>
                </>
    );
}

export default Authenticated;