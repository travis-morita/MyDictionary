import React, { useState, useEffect } from 'react';
import authService from './api-authorization/AuthorizeService'

const url = 'https://localhost:5000/MyWords/GetWordsJson';

const UserWordList = () => {
    const [user, setUser] = useState([]);

    const [words, setWords] = useState([]);

    const getUsers = async () => {

        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);
        setUser(user);
        console.log(user.sid);
        const getWords = async () => {
            const response = await fetch(`${url}/${user.sub}`);
            const words = await response.json();
            setWords(words);
        };
        getWords();
    };


    

    useEffect(() => {
       getUsers();
    }, []);

    //useEffect(() => {
    //    getWords();
    //}, []);

        return (
        <>
                <div class="row">
                    <div class="col-sm-4">
                        <form id="form-delete" method="post" asp-controller="MyWords" asp-action="DeleteFromList">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">
                                            Word
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {
                                        words.map((word) => {
                                            const { spelling } = word;
                                            return (
                                                <tr>
                                                    <td>
                                                        <a href="#"
                                                            data-poload="/Home/GetWordJson/{spelling}" data-toggle="popover">{spelling}</a>
                                                    </td>
                                                    <td>
                                <button id="{spelling}" type="button" value="Delete" class="btn delete-word" data-target="#confirm-delete" data-toggle="modal">
                                                            <i class="fa fa-trash"></i>
                                                        </button>
                                                    </td>
                                                </tr>
                                            );
                                        })
                                    }

                                </tbody>
                            </table>
                        </form>
                        <a href="#" id="qoo" rel="popover" class="circle"> Click to popover</a>
        <div class="modal fade" id="confirm-delete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                    </div>
                                    <div class="modal-body">
                                        Are you sure you want to remove this word from your list?
                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                        <button id="confirm" type="button" class="btn btn-danger btn-ok">Delete</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </>
    );
}


export default UserWordList;