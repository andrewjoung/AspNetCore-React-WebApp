import React, { useEffect, useState } from 'react';
import {
    IColumn,
    Spinner,
    DetailsList,
    DetailsListLayoutMode,
    SpinnerSize,
    PrimaryButton,
    DefaultButton
} from 'office-ui-fabric-react';
import { ILibraryDto, ApiClient } from '../../generated/backend';
import LibraryForm from './LibraryForm';

const Libraries: React.FC = () => {

    const [data, setData] = useState({
        libraries: [] as ILibraryDto[],
        isFetching: false
    });

    const [addMode, setAddMode] = useState(false);

    const libraryKeys: ILibraryDto = {
        id: null,
        name: '',
        address: null,
        createdDate: null,
        updatedDate: null
    };

    const columns = Object.keys(libraryKeys).map(
        (key): IColumn => {
            return {
                key,
                name: key.replace(/([A-Z])/g, ' $1').replace(/^./, (str: string) => {
                    return str.toUpperCase();
                }),
                fieldName: key,
                minWidth: 100,
                maxWidth: 200,
                isResizable: true
            };
        }
    );

    const handleCreateLibrary = (library: ILibraryDto) => {
        setData({libraries: [...data.libraries, library], isFetching: false});
    }

    useEffect(() => {
        const fetchData = async() => {
            try {
                setData({ libraries: data.libraries, isFetching: true});
                
                const result = await new ApiClient(
                    process.env.REACT_APP_API_BASE
                ).libraries_GetAllLibrary();
                
                console.log("results: " + result); 

                setData({ libraries: result, isFetching: false});
            } catch (e) {
                console.log(e);
                setData({ libraries: data.libraries, isFetching: false});
            }
        }

        fetchData();
        console.log(data.libraries);
    }, []);



    return (
        <>
            <h2> Libraries </h2>
            <DetailsList 
                items={data.libraries.map(library => {
                    return {
                        id: library.id,
                        name: library.name,
                        address: library.address.locationAddressLine1 + ", " + library.address.locationCity 
                            + ", " + library.address.locationStateProvince + " " + library.address.locationZipCode
                            + " " + library.address.locationCountry,
                        createdDate: library.createdDate.toLocaleString(),
                        updateddate: library.updatedDate.toLocaleString()
                    }
                })}
                columns={columns}
                layoutMode={DetailsListLayoutMode.justified}
            />
            {data.isFetching && <Spinner size={SpinnerSize.large} />}
            
            {addMode && <LibraryForm setAddMode={setAddMode} handleCreateLibrary={handleCreateLibrary} />}
            {!addMode && 
                <DefaultButton 
                    text="Add Library" 
                    style={{marginTop:'2em', marginBottom: '2em'}} 
                    onClick={()=>{setAddMode(true)}} 
                />
            }
        </>
    );
}

export default Libraries;