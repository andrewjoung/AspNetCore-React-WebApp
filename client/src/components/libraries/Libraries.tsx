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
import { ILibraryDto, ApiClient, LibraryDto, Address } from '../../generated/backend';
import LibraryForm from './LibraryForm';

// Library component that contains all the Libraries data 
const Libraries: React.FC = () => {

    // Library state 
    // Property data consists of libraries array
    // And isFetching boolean
    const [data, setData] = useState({
        libraries: [] as ILibraryDto[],
        isFetching: false
    });

    // Library state
    // addMode property lets application know if user is adding a new library or not
    const [addMode, setAddMode] = useState(false);

    // Initialize library keys
    const libraryKeys: ILibraryDto = {
        id: null,
        name: '',
        address: null,
        createdDate: null,
        updatedDate: null
    };

    // Initialize columns
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
    
    // Handler function for creating and saving a new library
    // Takes in a library object of type ILibraryDto
    const handleCreateLibrary = async(library: ILibraryDto) => {

            // Convert ILibraryDto to a LibraryDto to pass into API Client
            let libDto = LibraryDto.fromJS(library);

            // Create new API Client and call the create library function
            const result = await new ApiClient(
                process.env.REACT_APP_API_BASE
            ).libraries_CreateLibrary(libDto).then(res => {
                console.log(res);
                //return res

                // Add the new library to the state libraries array
                setData({libraries: [... data.libraries, libDto], isFetching: false});
                //return res;
            }).catch(err => {
                console.log(err);
                return err;
            })

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