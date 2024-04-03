import React from "react";
import {
  useGetNames,
  useLogout,
  useRejectNames,
} from "../../hooks/authentication-hooks";

function Authenticated() {
  const { data: names } = useGetNames();

  const { data: allNames } = useGetNames(0);

  const [selectedNames, setSelectedNames] = React.useState<number[]>([]);

  function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
    const name = Number(event.target.name);
    const nextSelectedNames = [...selectedNames];
    if (selectedNames.includes(name)) {
      nextSelectedNames.splice(selectedNames.indexOf(name), 1);
    } else {
      nextSelectedNames.push(name);
    }
    setSelectedNames(nextSelectedNames);
  }

  const logoutQuery = useLogout();

  const rejectNamesMutation = useRejectNames();

  return (
    <>
      <form>
        <ul>
          {names?.map((name) => (
            <li>
              <label>
                <input
                  type="checkbox"
                  checked={selectedNames.includes(name.id)}
                  name={name.id.toString()}
                  onChange={handleChange}
                />
                {name.name}
              </label>
            </li>
          ))}
        </ul>
        <button
          onClick={(event) => {
            event.preventDefault();
            rejectNamesMutation.mutate(selectedNames);
          }}
        >
          Reject names
        </button>
      </form>
      <button
        onClick={() => {
          logoutQuery.mutate();
        }}
      >
        Log out
      </button>
      <p>Antall navn som gjenstår: {allNames?.length}</p>
    </>
  );
}

export default Authenticated;
