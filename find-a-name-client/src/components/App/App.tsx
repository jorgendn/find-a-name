import { useGetNames } from "../../hooks/authentication-hooks";
import Authenticated from "../Authenticated";
import Login from "../Login";

function App() {
  const { isSuccess, isPending } = useGetNames();

  if (isPending) {
      return <p>Loading...</p>;
  }

  return (
      <>
          {isSuccess ? <Authenticated /> : <Login />}
      </>
  );
}

export default App
