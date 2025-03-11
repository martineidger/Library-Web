import React, { useContext } from 'react';
//import UserProfile from '../components/UserProfile';

const UserProfilePage = () => {
  const { user } = useContext(AuthContext);

  return (
    <div>
      <h1>Личный кабинет</h1>
      {/* {user ? <UserProfile user={user} /> : <p>Пожалуйста, войдите в систему.</p>} */}
    </div>
  );
};

export default UserProfilePage;