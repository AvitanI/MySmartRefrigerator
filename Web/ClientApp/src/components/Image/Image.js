/* React */
import React, { useState, useEffect } from 'react';

/* Material UI */
import BrokenImageIcon from '@material-ui/icons/BrokenImage';

const Image = (props) => {
    // Props
    const { src = '', fallbackSrc = '' } = props;

    // State
    const [readerResult, setReaderResult] = useState('');

    /*
     * Load image from given url
     */
    const tryToloadImage = async () => {
        try {
            const response = await fetch(src);

            if(!response.ok) {
                throw new Error('Could not load image');
            }

            const blob = await response.blob();

            var reader = new FileReader();
            // Base64 data URI
            reader.onload = () => { setReaderResult(reader.result) };
            reader.readAsDataURL(blob);
        }
        catch(err) {
            setReaderResult('');
        }
    };

    useEffect(() => {
        tryToloadImage();
    }, [src]);

    /*
        Renders fallback image if the required one not found
    */
    const renderFallback = () => {
        return (
            <div>
                {
                    (fallbackSrc && fallbackSrc.trim()) ? 
                        <img src={fallbackSrc} alt="Image not found" />
                        :
                        <BrokenImageIcon fontSize="large" />
                }
            </div>
        );
    };

    return (
        <div>
            {
                (!!(readerResult && readerResult.trim())) ? 
                    <img src={readerResult} 
                        alt="Image not found"
                        style={{ width: '50%' }}></img>
                    :
                    renderFallback()
            }
        </div>
    );
};

export default Image;