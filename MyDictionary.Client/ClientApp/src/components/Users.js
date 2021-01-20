//import React, { useEffect, useState } from 'react';

//const url = 'https://api.github.com/users';

//const Users = () => {
//    const [users, setUserss] = useState([]);

//    const getUserss = async () => {
//        const response = await fetch(url);
//        const users = await response.json();
//        setUserss(users);
//    };

//    useEffect(() => {
//        getUserss();
//    }, []);

//    return (
//        <>
//            <h3>github users</h3>
//            <ul>
//                {
//                    users.map((user) => {
//                        const { id, login, avatar_url, html_url } = user;
//                        return (
//                            <li key={id}>
//                                <img src={avatar_url} alt={login} />
//                                <div>
//                                    <h4>{login}</h4>
//                                    <a href={html_url}>profile</a>
//                                </div>
//                            </li>
//                        );
//                    })
//                }
//            </ul>
//        </>
//    );
//}


//export default Users;