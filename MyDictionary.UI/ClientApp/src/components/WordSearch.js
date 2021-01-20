import React, { useState } from 'react';
import authService from './api-authorization/AuthorizeService';
import { FaSearch } from 'react-icons/fa';
import WordMeta from './WordMeta';

const url = '/Home/GetWord';

const WordSearch = (props) => {
    const [wordSearch, setWord] = useState();
    const [wordMeta, setWordMeta] = useState([]);
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (wordSearch) {
            const token = await authService.getAccessToken();
            await fetch(`${url}/${wordSearch}`, {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
            })
                .then(response => {
                    console.log('1', response);
                    response.json().then(data => setWordMeta(data));
                    console.log('2', { wordMeta });
                });
                ///*.then(data => {
                //    console.log(data);
                //    setWordMeta(data);
                //});*/


            //setWordMeta(await response.json());
        }
    };

    return (
        <>
            <div className="col-md-4">
                <form className='form' onSubmit={handleSubmit}>
                    <div role="heading"><div className="mb-3">What word do you want to look up?</div></div>
                    <div className="input-group mb-3">
                        <input
                            type='text'
                            id='wordSearch'
                            value={wordSearch}
                            onChange={(e) => setWord(e.target.value)}
                            placeholder="Search for a word"
                            className="form-control"
                        />
                        <span className="input-group-btn">
                            <button className="btn btn-primary" type="submit">
                                <FaSearch />
                            </button>
                        </span>
                    </div>
               
                </form>
            </div>
            {wordMeta !== undefined && wordMeta.word !== undefined ? <WordMeta wordMeta={wordMeta} /> : null}
        </>
    );
}

export default WordSearch;