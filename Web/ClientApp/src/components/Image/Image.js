/* React */
import React, { useState, useEffect } from 'react';

/* Material UI */
import BrokenImageIcon from '@material-ui/icons/BrokenImage';
import CircularProgress from '@material-ui/core/CircularProgress';

const Image = (props) => {
    // Props
    const { src = '', fallbackSrc = '' } = props;

    // State
    const [readerResult, setReaderResult] = useState('');
    const [loading, setLoading] = useState(false);

    /*
     * Load image from given url
     */
    const tryToloadImage = async () => {
        setLoading(true);

        try {
            const response = await fetch(src);

            if(!response.ok) {
                throw new Error('Could not load image');
            }

            const blob = await response.blob();

            var reader = new FileReader();
            // Base64 data URI
            reader.onload = () => { 
                setReaderResult(reader.result);
                setLoading(false);
            };

            reader.readAsDataURL(blob);
        }
        catch(err) {
            setReaderResult('');
            setLoading(false);
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

    const renderImage = () => {
        return (
            <div>
                {
                    (!!(readerResult && readerResult.trim())) ? 
                        <img src={readerResult} 
                            alt="Image not found"
                            style={{ width: '100%', height: 'auto' }}></img>
                        :
                        renderFallback()
                }
            </div>
        );
    };

    return (
        <div style={{ textAlign: 'center' }}>
            {
                (loading) ? <CircularProgress /> : renderImage()
            }
        </div>
    );
};

export default Image;