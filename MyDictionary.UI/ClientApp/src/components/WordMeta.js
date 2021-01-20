import React, { useState, useEffect }  from 'react'; 
import authService from './api-authorization/AuthorizeService';
import { FaBookmark } from 'react-icons/fa';
import { FaRegBookmark } from 'react-icons/fa';




const WordMeta = (props) => {
    const [isSaved, setSaved] = useState(props.wordMeta.isSaved);
    console.log('isSaved', isSaved);
    console.log(props);
    const handleSubmit = async (e) => {
        e.preventDefault();

        let word = props.wordMeta.word[0].id;
        const url = (isSaved  ? `/Home/Delete/${word}` : '/Home/Add');
        console.log('url', url);
        const token = await authService.getAccessToken();

        await fetch(url, {
            headers: !token ? {} : {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
            method: (isSaved ? 'delete' : 'post'),
            body: `"${word}"`
        })
            .then(response => {
                response.text()
                    .then(data => {
                        if(data !== '') {
                            setSaved(true);
                        } else {
                            setSaved(false);
                        }
                    })
                    .catch(err => console.log('err 1', err));
                
            })
            .catch(err => console.log('err 2', err));
            

    };


    return (
        <>
            <div className="col-md-6">
                <h3>{props.wordMeta.word[0].id}</h3>
                <form className='form' onSubmit={handleSubmit}>
                    <button id="btnSave" type="submit">
                        {isSaved ? <FaBookmark /> : <FaRegBookmark />}
                    </button>
                </form>
                {props.wordMeta.word.map((word, index) => {
                    console.log('index', index);
                    const { partOfSpeech, definitions, pronunciations } = word;
                    return (

                        <div key={index} className="col-md-12">

                            <div>
                                <div className="col-md-12">
                                    <span style={{ fontStyle: 'italic' }}>
                                        {partOfSpeech}
                                    </span>
                                </div>
                                <ol>
                                    {definitions.map((def, index) => {
                                        return (
                                            <li key={index}>{def}</li>
                                        );
                                    })}
                                </ol>

                            </div>
                        </div>

                    );
                })}
            </div>
        </>
    );
}

export default WordMeta;